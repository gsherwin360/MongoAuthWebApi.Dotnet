using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.Commands;
using MongoAuthWebApi.MongoDb.Identity;
using MongoAuthWebApi.Primitives;
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

    [Fact]
    public async Task Should_Return_UserAlreadyExist_Error_Code_When_User_Already_Exist()
    {
        // Arrange
        string fakeEmail = MongoAuthWebApiTests.Faker.Person.Email;
        string fakePassword = MongoAuthWebApiTests.Faker.Internet.Password();

        var existingUser = new MongoUser()
        {
            Email = fakeEmail
        };

        var userManagerMock = MockFactory.GenerateMockUserManager();
        userManagerMock.Setup(m => m.FindByEmailAsync(fakeEmail)).ReturnsAsync(existingUser);

        var command = new RegisterCommand(fakeEmail, fakePassword);
        var handler = new RegisterCommandHandler(userManagerMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeEquivalentTo(AuthenticationError.UserAlreadyExist);
        userManagerMock.Verify(m => m.FindByEmailAsync(fakeEmail), Times.Once);
        userManagerMock.Verify(m => m.CreateAsync(It.IsAny<MongoUser>(), fakePassword), Times.Never);
    }

    [Fact]
    public async Task Should_Return_Identity_Error_Code_When_Registration_Fails()
    {
        // Arrange
        string fakeEmail = MongoAuthWebApiTests.Faker.Person.Email;
        string fakePassword = MongoAuthWebApiTests.Faker.Internet.Password();

        var userManagerMock = MockFactory.GenerateMockUserManager();
        userManagerMock.Setup(m => m.FindByEmailAsync(fakeEmail)).ReturnsAsync((MongoUser?)null);
        userManagerMock.Setup(m => m.CreateAsync(It.IsAny<MongoUser>(), fakePassword))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError
            {
                Code = MongoAuthWebApiTests.FakeErrorCode,
                Description = MongoAuthWebApiTests.FakeErrorMessage
            }));

        var command = new RegisterCommand(fakeEmail, fakePassword);
        var handler = new RegisterCommandHandler(userManagerMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be(MongoAuthWebApiTests.FakeErrorCode);
        result.Error.Message.Should().Be(MongoAuthWebApiTests.FakeErrorMessage);
        userManagerMock.Verify(m => m.FindByEmailAsync(fakeEmail), Times.Once);
        userManagerMock.Verify(m => m.CreateAsync(It.IsAny<MongoUser>(), fakePassword), Times.Once);
    }
}