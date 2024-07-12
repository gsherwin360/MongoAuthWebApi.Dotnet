namespace MongoAuthWebApi.MongoDb.Authentication;

public record AuthenticationResult
{
    public AuthenticationResult(string token, object user)
    {
        this.Token = token;
        this.User = user;
    }

    public string Token { get; private set; } = string.Empty;

    public object User { get; private set; }
}
