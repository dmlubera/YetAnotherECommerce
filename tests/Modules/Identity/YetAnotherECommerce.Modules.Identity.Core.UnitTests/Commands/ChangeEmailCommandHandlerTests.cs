using Moq;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{
    public class ChangeEmailCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IEventDispatcher> _eventDispatcherMock;
        private readonly ChangeEmailCommandHandler _handler;

        public ChangeEmailCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _eventDispatcherMock = new Mock<IEventDispatcher>();
            _handler = new ChangeEmailCommandHandler(_repositoryMock.Object, _eventDispatcherMock.Object);
        }

        [Fact]
        public async Task WhenProvidedEmailIsExactlyTheSameAsTheCurrentOne_ThenShouldThrowAnException()
        {

        }

        [Fact]
        public async Task WhenProvidedEmailHasInvalidFormat_ThenShouldThrowAnException()
        {

        }

        [Fact]
        public async Task WhenProvidedEmailIsAlreadyInUse_ThenShouldThrowAnException()
        {

        }

        [Fact]
        public async Task WhenProvidedEmailIsValid_ThenShouldUpdateEmailAndPublishEvent()
        {

        }
    }
}