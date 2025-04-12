using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands;

public class SignInCommandHandlerTests
{
    private readonly Mock<IAuthManager> _authManagerMock = new();
    private readonly Mock<ICache> _cacheMock = new();
    private readonly Mock<FakeUserManager> _userManagerMock = new();
    private readonly SignInCommandHandler _handler;

    public SignInCommandHandlerTests()
    {
        _handler = new SignInCommandHandler(_authManagerMock.Object, _cacheMock.Object, _userManagerMock.Object);
    }

    [Theory, AutoData]
    public async Task WhenUserWithGivenEmailDoesNotExist_ThenShouldThrowAnException(SignInCommand command)
    {
        // Arrange
        var expectedException = new InvalidCredentialsException();
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            
        // Act
        var result = await Assert.ThrowsAsync<InvalidCredentialsException>(() => _handler.HandleAsync(command));

        // Assert
        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(expectedException.ErrorCode);
        result.Message.ShouldBe(expectedException.Message);
    }

    [Theory, AutoData]
    public async Task WhenUserExistButPasswordIsInvalid_ThenShouldThrowAnException(SignInCommand command)
    {
        // Arrange
        var expectedException = new InvalidCredentialsException();
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await Assert.ThrowsAsync<InvalidCredentialsException>(() => _handler.HandleAsync(command));

        // Assert
        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(expectedException.ErrorCode);
        result.Message.ShouldBe(expectedException.Message);
    }
        
    [Theory, AutoData]
    public async Task WhenCredentialsAreValid_ThenShouldReturnToken(SignInCommand command)
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _cacheMock.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<JsonWebToken>()));
    }
}