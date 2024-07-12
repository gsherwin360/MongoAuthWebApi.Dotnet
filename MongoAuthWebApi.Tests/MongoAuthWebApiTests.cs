using Bogus;

namespace MongoAuthWebApi.Tests;

public class MongoAuthWebApiTests
{
	public static Faker Faker => new Faker();

	public const string FakeErrorCode = "FakeErrorCode";
	public const string FakeErrorMessage = "FakeErrorMessage";
}
