using MongoAuthWebApi.Primitives;

namespace MongoAuthWebApi.MongoDb.Authentication;

public interface IJwtAuthenticationService
{
    Task<Result<AuthenticationResult>> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken = default);
}