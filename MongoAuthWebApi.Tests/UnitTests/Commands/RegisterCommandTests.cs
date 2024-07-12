using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.Commands;
using MongoAuthWebApi.MongoDb.Identity;
using Moq;

namespace MongoAuthWebApi.Tests.UnitTests.Commands;

public class RegisterCommandTests
{
    [Fact]
    public async Task Should_Register_User_When_Command_Is_Handled_Successfully()
    {
        // Arrange
        string fakeEmail = MongoAuthWebApiTests.Faker.Person.Email;
        string fakePassword = MongoAuthWebApiTests.Faker.Internet.Password();

        var userManagerMock = MockFactory.GenerateMockUserManager();
        userManagerMock.Setup(m => m.FindByEmailAsync(fakeEmail)).ReturnsAsync((MongoUser?)null);
        userManagerMock.Setup(m => m.CreateAsync(It.IsAny<MongoUser>(), fakePassword)).ReturnsAsync(IdentityResult.Success);

        var command = new RegisterCommand(fakeEmail, fakePassword);
        var handler = new RegisterCommandHandler(userManagerMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        userManagerMock.Verify(m => m.FindByEmailAsync(fakeEmail), Times.Once);
        userManagerMock.Verify(m => m.CreateAsync(It.IsAny<MongoUser>(), fakePassword), Times.Once);
    }
}