using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.UnitTests.Fixtures.Commands;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Commands
{
    public class SignInCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IAuthManager> _authManagerMock;
        private readonly Mock<ICache> _cacheMock;
        private readonly SignInCommandHandler _handler;

        public SignInCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _cacheMock = new Mock<ICache>();
            _authManagerMock = new Mock<IAuthManager>();
            _handler = new SignInCommandHandler(_repositoryMock.Object, _authManagerMock.Object, _cacheMock.Object);
        }

        [Fact]
        public async Task WhenUserWithGivenEmailDoesNotExist_ThenShouldThrowAnException()
        {
            var commandFixture = SignInCommandFixture.Create();
            var expectedException = new InvalidCredentialsException();
            _repositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var result = await Assert.ThrowsAsync<InvalidCredentialsException>(() => _handler.HandleAsync(commandFixture));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task WhenUserExistButPasswordIsInvalid_ThenShouldThrowAnException()
        {
            var commandFixture = SignInCommandFixture.Create();
            var expectedException = new InvalidCredentialsException();
            var user = new User(commandFixture.Email, "incorrectPassword", "customer");
            _repositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var result = await Assert.ThrowsAsync<InvalidCredentialsException>(() => _handler.HandleAsync(commandFixture));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }
        
        [Fact]
        public async Task WhenCredentialsAreValid_ThenShouldReturnToken()
        {
            var commandFixture = SignInCommandFixture.Create();
            var user = new User(commandFixture.Email, commandFixture.Password, "customer");
            _repositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _authManagerMock.Setup(x => x.GenerateJwtToken(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(new JsonWebToken());

            await _handler.HandleAsync(commandFixture);

            _cacheMock.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<JsonWebToken>()));
        }
    }
}