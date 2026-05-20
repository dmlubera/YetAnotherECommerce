using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Catalog.Core.Exceptions;
using YetAnotherECommerce.Modules.Catalog.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Catalog.UnitTests.ValueObjects;

public class PriceTests
{
    [Fact]
    public void Create_WhenPriceIsLesserThanZero_ThenShouldThrowException()
    {
        var expectedException = new InvalidPriceException();

        var exception = Assert.Throws<InvalidPriceException>(() => Price.Create(-10));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public void Create_WhenPriceIsGreaterThanZero_ThenShouldReturnValueObject()
    {
        var price = 10;

        var result = Price.Create(price);

        result.ShouldNotBeNull();
        result.Value.ShouldBe(price);
    }
}