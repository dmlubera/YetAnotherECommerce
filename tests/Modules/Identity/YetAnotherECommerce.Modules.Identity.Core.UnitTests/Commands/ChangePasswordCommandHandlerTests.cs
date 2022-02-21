using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{
    public class ChangePasswordCommandHandlerTests
    {
        private readonly Mock<IUserService> _repositoryMock;
        private readonly ChangePasswordCommandHandler _handler;

        public ChangePasswordCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserService>();
            _handler = new ChangePasswordCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task ShouldInvokeChangePasswordMethodFromUserService()
        {
            var command = new ChangePasswordCommand(Guid.NewGuid(), "super$ecret");

            await _handler.HandleAsync(command);

            _repositoryMock
                .Verify(x => x.ChangePasswordAsync(It.IsIn(command.UserId), It.IsIn(command.Password)), Times.Once);
        }
    }
}