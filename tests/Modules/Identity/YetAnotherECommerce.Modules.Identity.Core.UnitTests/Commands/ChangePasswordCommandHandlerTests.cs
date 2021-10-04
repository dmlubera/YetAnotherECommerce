using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
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
            var command = new ChangePasswordCommand(Guid.NewGuid(), "");
            var expectedException = new InvalidPasswordFormatException();

            var result = await Assert.ThrowsAsync<InvalidPasswordFormatException>(() => _handler.HandleAsync(command));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenProvidedPasswordIsExactlyTheSameAsTheCurrentOne_ThenShouldThrowAnException()
        {
            var command = new ChangePasswordCommand(Guid.NewGuid(), "super$ecret");
            var user = new User("admin@yetanotherecommerce.com", command.Password, "admin");
            var expectedException = new ProvidedPasswordIsExactlyTheSameAsTheCurrentOne();
            _repositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var result =
                await Assert.ThrowsAsync<ProvidedPasswordIsExactlyTheSameAsTheCurrentOne>(() => _handler.HandleAsync(command));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenUserWithProvidedIdDoesNotExist_ThenShouldThrowAnException()
        {
            var command = new ChangePasswordCommand(Guid.NewGuid(), "super$ecret");
            var expectedException = new UserNotExistException(command.UserId);
            _repositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var result =
                await Assert.ThrowsAsync<UserNotExistException>(() => _handler.HandleAsync(command));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenProvidedPasswordIsValid_ThenShouldUpdateEntityInDatabase()
        {
            var command = new ChangePasswordCommand(Guid.NewGuid(), "super$ecret");
            var user = new User("admin@yetanotherecommerce.com", "previousOne", "admin");
            _repositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            await _handler.HandleAsync(command);

            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()));
        }
    }
}