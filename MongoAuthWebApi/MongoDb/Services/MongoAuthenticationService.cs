using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoAuthWebApi.MongoDb.Authentication;
using MongoAuthWebApi.MongoDb.Configuration;
using MongoAuthWebApi.MongoDb.Identity;
using MongoAuthWebApi.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MongoAuthWebApi.MongoDb.Services;

public class MongoAuthenticationService : IJwtAuthenticationService
{
    private readonly UserManager<MongoUser> _userManager;
    private readonly IOptions<JwtConfig> _jwtConfigOptions;

    public MongoAuthenticationService(UserManager<MongoUser> userManager, IOptions<JwtConfig> jwtConfigOptions)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		_jwtConfigOptions = jwtConfigOptions ?? throw new ArgumentNullException(nameof(jwtConfigOptions));
    }

    public async Task<Result<AuthenticationResult>> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(email);

        if (user is null)
        {
            return Result<AuthenticationResult>.Failure(AuthenticationError.InvalidCredentials);
        }

        if (user.IsLockedOut)
        {
            return Result<AuthenticationResult>.Failure(AuthenticationError.UserLockedOut);
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (isValidPassword)
        {
            user.LastActivityOn = DateTime.UtcNow;
            await _userManager.ResetAccessFailedCountAsync(user);

            var issuer = _jwtConfigOptions.Value.ValidIssuer;
            var audience = _jwtConfigOptions.Value.ValidAudience;
            var key = Encoding.ASCII.GetBytes(_jwtConfigOptions.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(this._jwtConfigOptions.Value.TokenValidityInMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return Result<AuthenticationResult>.Success(new AuthenticationResult(stringToken, user));
        }

        await this._userManager.AccessFailedAsync(user);
        return Result<AuthenticationResult>.Failure(AuthenticationError.InvalidCredentials);
    }
}