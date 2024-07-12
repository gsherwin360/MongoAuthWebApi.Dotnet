using MediatR;
using MongoAuthWebApi.Models.DTOs;
using MongoAuthWebApi.MongoDb.Authentication;
using MongoAuthWebApi.Primitives;

namespace MongoAuthWebApi.Commands;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginDTO>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginDTO>>
{
    private readonly IJwtAuthenticationService _jwtAuthenticationService;

    public LoginCommandHandler(IJwtAuthenticationService jwtAuthenticationService)
    {
        _jwtAuthenticationService = jwtAuthenticationService ?? throw new ArgumentNullException(nameof(jwtAuthenticationService));
    }

    public async Task<Result<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginResult = await _jwtAuthenticationService.AuthenticateUserAsync(request.Email, request.Password, cancellationToken);

        if (loginResult.IsSuccess)
        {
            return Result<LoginDTO>.Success(new LoginDTO(loginResult.Value));
        }

        return Result<LoginDTO>.Failure(loginResult.Error);
    }
}
