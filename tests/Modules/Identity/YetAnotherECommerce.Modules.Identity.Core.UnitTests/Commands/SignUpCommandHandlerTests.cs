using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Fixtures.Commands;
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

    [Fact]
    public async Task WhenUserSuccessfullyCreate_ThenShouldPublishEvent()
    {
        // Arrange
        var command = SignUpCommandFixture.Create();
        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<UserRegistered>()));
    }
}