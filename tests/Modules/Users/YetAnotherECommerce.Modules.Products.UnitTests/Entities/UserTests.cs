using Moq;
using Shouldly;
using System.Linq;
using Xunit;
using YetAnotherECommerce.Modules.Users.Core.DomainEvents;
using YetAnotherECommerce.Modules.Users.Core.Entities;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Entities
{
    public class UserTests
    {
        [Fact]
        public void UpdateAddress_ShouldUpdateAndAddDomainEvent()
        {
            var user = Mock.Of<User>();
            var street = "Grove Street";
            var city = "Los Santos";
            var zipCode = "555-1111";
            var country = "USA";

            user.UpdateAddress(street, city, zipCode, country);

            user.Address.Street.ShouldBe(street);
            user.Address.City.ShouldBe(city);
            user.Address.ZipCode.ShouldBe(zipCode);
            user.Address.Country.ShouldBe(country);
            user.Events.FirstOrDefault().ShouldBeOfType<AddressChanged>();
        }

        [Fact]
        public void UpdateFirstName_ShouldUpdateAndAddDomainEvent()
        {
            var user = Mock.Of<User>();
            var firstName = "Carl";
            user.UpdateFirstName(firstName);

            user.FirstName.Value.ShouldBe(firstName);
            user.Events.FirstOrDefault().ShouldBeOfType<FirstNameChanged>();
        }

        [Fact]
        public void UpdateLastName_ShouldUpdateAndAddDomainEvent()
        {
            var user = Mock.Of<User>();
            var lastName = "Johnson";
            user.UpdateLastName(lastName);

            user.LastName.Value.ShouldBe(lastName);
            user.Events.FirstOrDefault().ShouldBeOfType<LastNameChanged>();
        }

        [Fact]
        public void UpdateEmail_ShouldUpdateAndAddDomainEvent()
        {
            var user = Mock.Of<User>();
            var email = "cj@yetanotherecommerce.com";
            user.UpdateEmail(email);

            user.Email.ShouldBe(email);
            user.Events.FirstOrDefault().ShouldBeOfType<EmailChanged>();
        }
    }
}
