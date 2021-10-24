using System;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Users.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Address Address { get; private set; }

        public User(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }

        public void UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidFirstNameValueException();

            FirstName = firstName;
        }

        public void UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidLastNameValueException();

            LastName = lastName;
        }

        public void UpdateAddress(string street, string city, string zipcode, string country)
            => Address = Address.Create(street, city, zipcode, country);
    }
}