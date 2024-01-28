using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Entities
{
    public class UserTests
    {
        [Theory]
        [InlineData("customer")]
        [InlineData("admin")]
        public void Create_WhenRoleIsValid_ThenShouldInitializeUserEntity(string role)
        {
            var email = Email.Create("test@yetanotherecommerce.com");
            var password = Password.Create("password");

            var user = User.Create(email, password, role);

            user.Email.Value.ShouldBe(email);
            user.Password.Hash.ShouldBe(password.Hash);
            user.Role.ShouldBe(role);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenRoleIsInvalid_ThenShouldThrowAnException(string role)
        {
            var email = Email.Create("test@yetanotherecommerce.com");
            var password = Password.Create("password");
            var expectedException = new RoleNotExistException(role);

            var exception = Assert.Throws<RoleNotExistException>(() => User.Create(email, password, role));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public void ChangeEmail_WhenEmailIsValid_ThenShouldUpdate()
        {
            var email = "test@yetanotherecommerce.com";
            var user = Mock.Of<User>();

            user.ChangeEmail(email);

            user.Email.Value.ShouldBe(email);
        }

        [Fact]
        public void ChangePassowrd_WhenPasswordIsValid_ThenShouldUpdate()
        {
            var password = Password.Create("password");
            var user = Mock.Of<User>();

            user.ChangePassword(password);

            user.Password.Hash.ShouldBe(password.Hash);
        }
    }
}