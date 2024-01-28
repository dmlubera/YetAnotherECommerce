using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.DomainServices
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }


        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task CreateUserAsync_WhenGivenEmailHasInvalidFormat_ThenShouldThrowsAnException(string email)
        {
            var expectedException = new InvalidEmailFormatException();

            var exception = await Assert.ThrowsAsync<InvalidEmailFormatException>(() => _userService.CreateUserAsync(email, "super$ecret", "admin"));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task CreateUserAsync_WhenGivenPasswordHasInvalidFormat_ThenShouldThrowsAnException(string password)
        {
            var expectedException = new InvalidPasswordFormatException();

            var exception = await Assert.ThrowsAsync<InvalidPasswordFormatException>(() => _userService.CreateUserAsync("test@yetanotherecommerce.com", password, "admin"));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WhenGivenEmailIsAlreadyInUse_ThenShouldThrowsAnException()
        {
            var expectedException = new EmailInUseException();
            _userRepositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<EmailInUseException>(() => _userService.CreateUserAsync("test@yetanotherecommerce.com", "super$ecret", "admin"));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WhenGivenDataIsValid_ShouldReturnsNewlyCreatedUser()
        {
            var email = "test@yetanotherecommerce.com";
            _userRepositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _userService.CreateUserAsync(email, "super$ecret", "admin");

            result.ShouldNotBeNull();
            result.Email.Value.ShouldBe(email);
        }

        [Fact]
        public async Task ChangePasswordAsync_WhenUserWithProvidedIdDoesNotExist_ThenShouldThrowAnException()
        {
            var userId = Guid.NewGuid();
            var expectedException = new UserNotExistException(userId);
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var result =
                await Assert.ThrowsAsync<UserNotExistException>(() => _userService.ChangePasswordAsync(userId, "super$ecret"));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ChangePasswordAsync_WhenGivenPaswordHasInvalidFormat_ThenShouldThrowAnException(string password)
        {
            var expectedException = new InvalidPasswordFormatException();
            var userMock = Mock.Of<User>();
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Mock.Of<User>(x => x.Password == Password.Create("password")));

            var result =
                await Assert.ThrowsAsync<InvalidPasswordFormatException>(() => _userService.ChangePasswordAsync(Guid.NewGuid(), password));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task ChangePasswordAsync_WhenGivenPasswordIsExactlyTheSameAsCurrentOne_ThenShouldThrowAnException()
        {
            var password = "super$ecret";
            var user = User.Create(Email.Create("admin@yetanotherecommerce.com"), Password.Create(password), "admin");
            var expectedException = new ProvidedPasswordIsExactlyTheSameAsTheCurrentOne();
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var result =
                await Assert.ThrowsAsync<ProvidedPasswordIsExactlyTheSameAsTheCurrentOne>(() => _userService.ChangePasswordAsync(Guid.NewGuid(), password));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task ChangePassowrdAsync_WhenProvidedPasswordHasValidFormat_ThenShouldUpdateEntityInDatabase()
        {
            var newPassword = "super$ecret";
            var user = User.Create(Email.Create("admin@yetanotherecommerce.com"), Password.Create("password"), "admin");
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            await _userService.ChangePasswordAsync(user.Id, newPassword);

            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ChangeEmailAsync_WhenEmailHasInvalidFormat_ThenShouldThrowsAnException(string email)
        {
            var expectedException = new InvalidEmailFormatException();

            var result =
                await Assert.ThrowsAsync<InvalidEmailFormatException>(() => _userService.ChangeEmailAsync(Guid.NewGuid(), email));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task ChangeEmailAsync_WhenEmailIsInUse_ThenShouldThrowsAnException()
        {
            var email = "test@yetanotherecommerce.com";
            var expectedException = new EmailInUseException();
            _userRepositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await Assert.ThrowsAsync<EmailInUseException>(() => _userService.ChangeEmailAsync(Guid.NewGuid(), email));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task ChangeEmailAsync_WhenUserNotExists_ThenShouldThrowsAnException()
        {
            var userId = Guid.NewGuid();
            var email = "test@yetanotherecommerce.com";
            var expectedException = new UserNotExistException(userId);
            _userRepositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var result = await Assert.ThrowsAsync<UserNotExistException>(() => _userService.ChangeEmailAsync(userId, email));

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(expectedException.ErrorCode);
            result.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public async Task ChangeEmailAsync_WhenDataIsValid_ThenShouldUpdateEmailAndSaveChangesToDatabase()
        {
            var newEmail = "test@yetanotherecommerce.com";
            var user = User.Create(Email.Create("admin@yetanotherecommerce.com"), Password.Create("passowrd"), "admin");
            _userRepositoryMock
                .Setup(x => x.CheckIfEmailIsInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            await _userService.ChangeEmailAsync(user.Id, newEmail);

            user.Email.Value.ShouldBe(newEmail);
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsIn(user)), Times.Once);
        }
    }
}