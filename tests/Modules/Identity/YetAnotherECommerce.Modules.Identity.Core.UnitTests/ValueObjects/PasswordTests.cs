using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.ValueObjects
{
    public class PasswordTests
    {
        [Fact]
        public void Create_WhenValidData_ThenShouldReturnEmailValueObject()
        {
            var hash = "passwordHash";
            var salt = "passwordSalt";

            var password = Password.Create(hash, salt);

            password.ShouldNotBeNull();
            password.Hash.ShouldBe(hash);
            password.Salt.ShouldBe(salt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenInvalidHash_ThenShouldThrowAnException(string hash)
        {
            var expectedException = new InvalidPasswordHashException();

            var exception = Assert.Throws<InvalidPasswordHashException>(() => Password.Create(hash, "salt"));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenInvalidSalt_ThenShouldThrowAnException(string salt)
        {
            var expectedException = new InvalidPasswordSaltException();

            var exception = Assert.Throws<InvalidPasswordSaltException>(() => Password.Create("hash", salt));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Theory]
        [InlineData("super$ecret", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("  ", false)]
        public void HasValidFormat_ShouldValidate(string password, bool expectedResult)
        {
            var result = Password.HasValidFormat(password);

            result.ShouldBe(expectedResult);
        }
    }
}