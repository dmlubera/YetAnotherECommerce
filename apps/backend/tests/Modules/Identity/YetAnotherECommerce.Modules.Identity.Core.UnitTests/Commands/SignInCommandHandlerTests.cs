using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.Dtos;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Services;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands;

public class SignInCommandHandlerTests
{
    private readonly Mock<IAuthManager> _authManagerMock = new();
    private readonly Mock<FakeUserManager> _userManagerMock = new();
    private readonly SignInCommandHandler _handler;

    public SignInCommandHandlerTests()
    {
        _handler = new SignInCommandHandler(_authManagerMock.Object, _userManagerMock.Object);
    }

    [Theory, AutoData]
    public async Task WhenUserWithGivenEmailDoesNotExist_ThenReturnFailedResult(
        [FixtureCustomization] SignInCommand command)
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            
        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSucceeded.ShouldBeFalse();
        result.Error.ShouldBeOfType<InvalidCredentialsError>();
        result.Value.ShouldBeNull();
    }

    [Theory, AutoData]
    public async Task WhenGivenPasswordIsNotValid_ThenReturnFailedResult(
        [FixtureCustomization] SignInCommand command)
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSucceeded.ShouldBeFalse();
        result.Error.ShouldBeOfType<InvalidCredentialsError>();
        result.Value.ShouldBeNull();
    }
        
    [Theory, AutoData]
    public async Task WhenCredentialsAreValid_ThenReturnSucceededResultWithAccessToken(
        [FixtureCustomization] SignInCommand command)
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync([Role.Customer]);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSucceeded.ShouldBeTrue();
        result.Error.ShouldBeNull();
        result.Value.ShouldBe(It.Is<JsonWebToken>(x => !string.IsNullOrWhiteSpace(x.AccessToken)));
    }
}