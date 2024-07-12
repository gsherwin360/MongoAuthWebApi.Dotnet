namespace MongoAuthWebApi.Primitives;

public static class AuthenticationError
{
    public static Error InvalidCredentials => new Error(
        "InvalidCredentials",
        "The email or password is incorrect.");

    public static Error UserLockedOut => new Error(
        "UserLockedOut",
        "The user account is temporarily locked for 5 minutes due to multiple failed login attempts. Please try again later.");

	public static Error UserAlreadyExist => new Error(
		"UserAlreadyExist",
		"User is already exist.");
}
