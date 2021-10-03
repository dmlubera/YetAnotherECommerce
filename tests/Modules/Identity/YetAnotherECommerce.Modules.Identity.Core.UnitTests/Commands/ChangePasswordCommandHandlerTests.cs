using Moq;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{
    public class ChangePasswordCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly ChangePasswordCommandHandler _handler;

        public ChangePasswordCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _handler = new ChangePasswordCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task WhenProvidedPasswordHasInvalidFormat_ThenShouldThrowAnException()
        {

        }

        [Fact]
        public async Task WhenProvidedPasswordIsExactlyTheSameAsTheCurrentOne_ThenShouldThrowAnException()
        {

        }

        [Fact]
        public async Task WhenUserWithProvidedIdDoesNotExist_ThenShouldThrowAnException()
        {

        }

        [Fact]
        public async Task WhenProvidedPasswordIsValid_ThenShouldUpdateEntityInDatabase()
        {

        }
    }
}