using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
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
    public async Task WhenUserSuccessfullyCreate_ThenShouldPublishEvent(SignUpCommand command)
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<UserRegistered>()));
    }
}