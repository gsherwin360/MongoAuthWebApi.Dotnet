using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.MongoDb.Identity;
using Moq;

namespace MongoAuthWebApi.Tests;

public static class MockFactory
{
	public static Mock<UserManager<MongoUser>> GenerateMockUserManager()
	{
		var userStoreMock = new Mock<IUserStore<MongoUser>>();
		return new Mock<UserManager<MongoUser>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
	}
}
