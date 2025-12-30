using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Users.UnitTests.ValueObjects;

public class LastNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WhenLastNameHasInvalidFormat_ThenShouldThrowExcepion(string lastName)
    {
        var expectedException = new InvalidLastNameValueException();

        var exception = Assert.Throws<InvalidLastNameValueException>(() => LastName.Create(lastName));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public void Create_WhenLastNameHasValidFormat_ThenShouldReturnValueObject()
    {
        var lastName = "Johnson";

        var result = LastName.Create(lastName);

        result.ShouldNotBeNull();
        result.Value.ShouldBe(lastName);
    }
}