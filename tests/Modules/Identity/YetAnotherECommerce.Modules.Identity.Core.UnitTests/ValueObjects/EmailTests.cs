using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Create_WhenValidData_ThenShouldReturnEmailValueObject()
        {
            var givenEmail = "test@yetanotherecommerce.com";
            var email = Email.Create(givenEmail);

            email.ShouldNotBeNull();
            email.Value.ShouldBe(givenEmail);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenEmailHasInvalidFormat_ThenShouldThrowException(string email)
        {
            var expectedException = new InvalidEmailFormatException();

            var exception = Assert.Throws<InvalidEmailFormatException>(() => Email.Create(email));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Theory]
        [InlineData("test@yetanotherecommerce.com", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("  ", false)]
        public void HasValidFormat_ShouldValidate(string email, bool expectedResult)
        {
            var result = Email.HasValidFormat(email);

            result.ShouldBe(expectedResult);
        }
    }
}