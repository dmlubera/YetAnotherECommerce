using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands;

public class ChangePasswordCommandHandlerTests
{
    private readonly Mock<FakeUserManager>  _userManagerMock = new();
    private readonly ChangePasswordCommandHandler _sut;
    
    public ChangePasswordCommandHandlerTests()
    {
        _sut = new ChangePasswordCommandHandler(_userManagerMock.Object);
    }

    [Theory, AutoData]
    public async Task WhenUserDoesNotExist_ThenThrowException(
        [FixtureCustomization] ChangePasswordCommand command)
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);
        var expectedException = new UserNotExistException(command.UserId);
        
        // Act
        var result = await Assert.ThrowsAsync<UserNotExistException>(() => _sut.HandleAsync(command));

        // Assert
        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(expectedException.ErrorCode);
        result.Message.ShouldBe(expectedException.Message);
    }
    
    [Theory, AutoData]
    public async Task WhenUserManagerReturnedError_ThenReturnFailedResult(
        [FixtureCustomization] ChangePasswordCommand command)
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        _userManagerMock
            .Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed());
        
        // Act
        var result = await _sut.HandleAsync(command);

        // Assert
        result.IsSucceeded.ShouldBeFalse();
        result.Error.ShouldBeOfType<ChangePasswordFailedError>();
    }
    
    [Theory, AutoData]
    public async Task WhenChangingPasswordSucceeded_ThenReturnSucceededResult(
        [FixtureCustomization] ChangePasswordCommand command)
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        _userManagerMock
            .Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        // Act
        var result = await _sut.HandleAsync(command);

        // Assert
        result.IsSucceeded.ShouldBeTrue();
        result.Error.ShouldBeNull();
    }
}