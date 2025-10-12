using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands;

public class SignUpCommandHandlerTests
{
    private readonly Mock<FakeUserManager> _userManagerMock = new();
    private readonly Mock<IMessageBroker> _messageBrokerMock = new();
    private readonly SignUpCommandHandler _handler;

    public SignUpCommandHandlerTests()
    {
        _handler = new SignUpCommandHandler(_userManagerMock.Object, _messageBrokerMock.Object);
    }

    [Theory, AutoData]
    public async Task WhenUserWithGivenEmailAlreadyExists_ThenReturnFailedResult(
        [FixtureCustomization] SignUpCommand command)
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        // Act
        var result = await _handler.HandleAsync(command);
        
        // Assert
        result.IsSucceeded.ShouldBeFalse();
        result.Error.ShouldBeOfType<SignUpFailedError>();
    }
    
    [Theory, AutoData]
    public async Task WhenUserManagerReturnedError_ThenReturnFailedResult(
        [FixtureCustomization] SignUpCommand command)
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed());
        
        // Act
        var result = await _handler.HandleAsync(command);
        
        // Assert
        result.IsSucceeded.ShouldBeFalse();
        result.Error.ShouldBeOfType<SignUpFailedError>();
    }

    [Theory, AutoData]
    public async Task WhenUserSuccessfullyCreated_ThenPublishEventAndReturnSucceededResult(
        [FixtureCustomization] SignUpCommand command)
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<UserRegistered>()));
        result.IsSucceeded.ShouldBeTrue();
        result.Error.ShouldBeNull();
    }
}