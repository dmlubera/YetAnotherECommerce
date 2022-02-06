using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.DomainServices
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEncrypter> _encrypterMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _encrypterMock = new Mock<IEncrypter>();
            _userService = new UserService(_userRepositoryMock.Object, _encrypterMock.Object);
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
            _encrypterMock
                .Setup(x => x.GetSalt())
                .Returns("salt");
            _encrypterMock
                .Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("hash");

            var result = await _userService.CreateUserAsync(email, "super$ecret", "admin");

            result.ShouldNotBeNull();
            result.Email.Value.ShouldBe(email);
        }
    }
}