using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Users.Core.Commands
{
    public class CompleteRegistrationCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public CompleteRegistrationCommand(Guid userId, string firstName,
            string lastName, string street, string city, string zipCode, string country)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }
    }
}