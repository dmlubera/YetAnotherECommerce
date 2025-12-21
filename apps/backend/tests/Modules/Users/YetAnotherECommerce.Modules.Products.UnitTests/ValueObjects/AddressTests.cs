using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.UnitTests.ValueObjects
{
    public class AddressTests
    {
        private const string ValidStreet = "Grove Street";
        private const string ValidCity = "Los Santos";
        private const string ValidZipCode = "555-1111";
        private const string ValidCountry = "USA";

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenStreetHasInvalidFormat_ThenShouldThrowAnException(string street)
        {
            AssertException<InvalidStreetValueException>(street, ValidCity, ValidZipCode, ValidCountry);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenCityHasInvalidFormat_ThenShouldThrowAnException(string city)
        {
            AssertException<InvalidCityValueException>(ValidStreet, city, ValidZipCode, ValidCountry);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenZipCodeHasInvalidFormat_ThenShouldThrowAnException(string zipCode)
        {
            AssertException<InvalidZipCodeValueException>(ValidStreet, ValidCity, zipCode, ValidCountry); ;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WhenCountryHasInvalidFormat_ThenShouldThrowAnException(string country)
        {
            AssertException<InvalidCountryValueException>(ValidStreet, ValidCity, ValidZipCode, country);
        }

        [Fact]
        public void Create_WhenValidData_ThenShouldReturnAddressValueObject()
        {
            var result = Address.Create(ValidStreet, ValidCity, ValidZipCode, ValidCountry);

            result.ShouldNotBeNull();
            result.Street.ShouldBe(ValidStreet);
            result.City.ShouldBe(ValidCity);
            result.ZipCode.ShouldBe(ValidZipCode);
            result.Country.ShouldBe(ValidCountry);
        }

        private static void AssertException<T>(string street, string city, string zipCode, string country)
            where T : YetAnotherECommerceException, new()
        {
            var expectedException = new T();

            var exception = Assert.Throws<T>(() => Address.Create(street, city, zipCode, country));

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }
    }
}