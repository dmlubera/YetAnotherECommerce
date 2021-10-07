using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Messages.Events;
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
        public async Task WhenProvidedEmailHasInvalidFormat_ThenShouldThrowAnException()
        {
            var command = new ChangeEmailCommand(Guid.NewGuid(), "");
            var expectedException = new InvalidEmailFormatException();

            var result = await Assert.ThrowsAsync<InvalidEmailFormatException>(() => _handler.HandleAsync(command));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenProvidedEmailIsAlreadyInUse_ThenShouldThrowAnException()
        {
            var command = new ChangeEmailCommand(Guid.NewGuid(), "test@yetanotherecommerce.com");
            var expectedException = new EmailInUseException();
            _repositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await Assert.ThrowsAsync<EmailInUseException>(() => _handler.HandleAsync(command));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenUserWithProvidedIdDoesNotExist_ThenShouldThrowAnException()
        {
            var command = new ChangeEmailCommand(Guid.NewGuid(), "test@yetanotherecommerce.com");
            var expectedException = new UserNotExistException(command.UserId);
            _repositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _repositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var result = await Assert.ThrowsAsync<UserNotExistException>(() => _handler.HandleAsync(command));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenProvidedEmailIsValid_ThenShouldUpdateEmailAndPublishEvent()
        {
            var command = new ChangeEmailCommand(Guid.NewGuid(), "test@yetanotherecommerce.com");
            var user = new User("admin@yetanotherecommerce.com", "super$ecret", "admin");
            _repositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _repositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            await _handler.HandleAsync(command);

            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()));
            _eventDispatcherMock.Verify(x => x.PublishAsync(It.IsAny<EmailChanged>()));
        }
    }
}