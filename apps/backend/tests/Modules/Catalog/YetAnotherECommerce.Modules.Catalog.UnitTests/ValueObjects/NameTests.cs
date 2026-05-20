using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Catalog.Core.Exceptions;
using YetAnotherECommerce.Modules.Catalog.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Catalog.UnitTests.ValueObjects;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WhenNameHasInvalidFormat_ThenShouldThrowExcepion(string name)
    {
        var expectedException = new InvalidProductNameException();

        var exception = Assert.Throws<InvalidProductNameException>(() => Name.Create(name));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public void Create_WhenFirstNameHasValidFormat_ThenShouldReturnValueObject()
    {
        var name = "Ultrabook";

        var result = Name.Create(name);

        result.ShouldNotBeNull();
        result.Value.ShouldBe(name);
    }
}