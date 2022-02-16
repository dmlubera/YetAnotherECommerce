using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Products.UnitTests.ValueObjects
{
    public class QuantityTests
    {
        [Fact]
        public void Create_WhenPriceIsLesserThanZero_ThenShouldThrowException()
        {
            var expectedException = new InvalidQuantityValueException();

            var exception = Assert.Throws<InvalidQuantityValueException>(() => Quantity.Create(-10));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public void Create_WhenPriceIsGreaterThanZero_ThenShouldReturnValueObject()
        {
            var quantity = 10;

            var result = Price.Create(quantity);

            result.ShouldNotBeNull();
            result.Value.ShouldBe(quantity);
        }
    }
}