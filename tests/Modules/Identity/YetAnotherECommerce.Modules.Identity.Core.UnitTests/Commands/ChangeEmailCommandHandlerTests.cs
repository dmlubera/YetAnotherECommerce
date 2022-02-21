using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{
    public class ChangeEmailCommandHandlerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly ChangeEmailCommandHandler _handler;

        public ChangeEmailCommandHandlerTests()
        {
            _userService = new Mock<IUserService>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _handler = new ChangeEmailCommandHandler(_userService.Object, _messageBrokerMock.Object);
        }

        [Fact]
        public async Task ShouldInvokeChangeEmailMethodFromUserService()
        {
            var command = new ChangeEmailCommand(Guid.NewGuid(), "test@yetanotherecommerce.com");

            await _handler.HandleAsync(command);

            _userService
                .Verify(x => x.ChangeEmailAsync(It.IsIn(command.UserId), It.IsIn(command.Email)), Times.Once);
        }
    }
}