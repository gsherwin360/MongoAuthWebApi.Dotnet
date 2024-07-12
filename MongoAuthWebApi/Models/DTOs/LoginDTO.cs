using MongoAuthWebApi.MongoDb.Authentication;
using MongoAuthWebApi.MongoDb.Identity;

namespace MongoAuthWebApi.Models.DTOs;

public class LoginDTO
{
    public string Token { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public DateTime LastActivityOn { get; set; }

    public LoginDTO(AuthenticationResult result)
    {
        if (result.User is not MongoUser user)
        {
            throw new InvalidOperationException("Cannot cast into MongoUser");
        }

        Token = result.Token;
        FirstName = user.FirstName;
        Surname = user.LastName;
        Email = user.Email;
        LastActivityOn = user.LastActivityOn;
    }

    public LoginDTO()
    {
    }
}
