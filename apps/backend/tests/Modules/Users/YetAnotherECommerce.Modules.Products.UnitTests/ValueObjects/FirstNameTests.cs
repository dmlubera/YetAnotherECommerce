using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Products.UnitTests.ValueObjects
{
    public class FirstNameTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenFirstNameHasInvalidFormat_ThenShouldThrowExcepion(string firstName)
        {
            var expectedException = new InvalidFirstNameValueException();

            var exception = Assert.Throws<InvalidFirstNameValueException>(() => FirstName.Create(firstName));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public void Create_WhenFirstNameHasValidFormat_ThenShouldReturnValueObject()
        {
            var firstName = "Carl";

            var result = FirstName.Create(firstName);

            result.ShouldNotBeNull();
            result.Value.ShouldBe(firstName);
        }
    }
}