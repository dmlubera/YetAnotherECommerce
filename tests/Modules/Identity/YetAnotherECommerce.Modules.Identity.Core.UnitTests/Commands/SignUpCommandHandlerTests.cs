using Moq;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Fixtures.Commands;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{

    public class SignUpCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly SignUpCommandHandler _handler;

        public SignUpCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _userServiceMock = new Mock<IUserService>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _handler = new SignUpCommandHandler(_repositoryMock.Object, _userServiceMock.Object, _messageBrokerMock.Object);
        }

        [Fact]
        public async Task WhenUserServiceReturnsUser_ThenShouldAddUserToDatabaseAndPublishEvent()
        {
            var command = SignUpCommandFixture.Create();
            var user = User.Create(Email.Create(command.Email), Password.Create("hash", "salt"), "admin");
            _userServiceMock
                .Setup(x => x.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            await _handler.HandleAsync(command);

            _repositoryMock.Verify(x => x.AddAsync(It.IsIn(user)));
            _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<UserRegistered>()));
        }
    }
}