using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Fixtures.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{

    public class SignUpCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly SignUpCommandHandler _handler;

        public SignUpCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _handler = new SignUpCommandHandler(_repositoryMock.Object, _messageBrokerMock.Object);
        }

        [Fact]
        public async Task WhenGivenEmailAlreadyInUse_ThenShouldThrowAnException()
        {
            var command = SignUpCommandFixture.Create();
            var expectedException = new EmailInUseException();
            _repositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<EmailInUseException>(async () => await _handler.HandleAsync(command));

            exception.Message.ShouldBe(expectedException.Message);
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task WhenGivenEmailHasInvalidFormat_ThenShouldThrowAnException(string invalidEmail)
        {
            var command = new SignUpCommand(invalidEmail, "super$ecret", "customer");
            var expectedException = new InvalidEmailFormatException();

            var exception = await Assert.ThrowsAsync<InvalidEmailFormatException>(async () => await _handler.HandleAsync(command));

            exception.Message.ShouldBe(expectedException.Message);
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        }

        [Fact]
        public async Task WhenGivenRoleIsInvalid_ThenShouldThrowAnException()
        {
            var command = new SignUpCommand("test@yetanotherecommerce.com", "super$ecret", "invalidRole");
            var expectedException = new RoleNotExistException(command.Role);

            var exception = await Assert.ThrowsAsync<RoleNotExistException>(async () => await _handler.HandleAsync(command));

            exception.Message.ShouldBe(expectedException.Message);
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task WhenGivenPasswordhHasInvalidFormat_ThenShouldThrowAnException(string invalidPassword)
        {
            var command = new SignUpCommand("test@yetanotherecommerce.com", invalidPassword, "customer");
            var expectedException = new InvalidPasswordFormatException();

            var exception = await Assert.ThrowsAsync<InvalidPasswordFormatException>(async () => await _handler.HandleAsync(command));

            exception.Message.ShouldBe(expectedException.Message);
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        }

        [Fact]
        public async Task WhenGivenDataIsCorrect_ThenShouldAddUserToDatabaseAndPublishEvent()
        {
            var command = SignUpCommandFixture.Create();
            _repositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            await _handler.HandleAsync(command);

            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()));
            _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<UserRegistered>()));
        }
    }
}