using System;
using YetAnotherECommerce.Modules.Users.Core.DomainEvents;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.Entities
{
    public class User : AggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Address Address { get; private set; }
        public bool IsRegistrationCompleted { get; private set; }

        private User() { }

        public User(Guid id, string firstName, string lastName, string email,
            string password, Address address, bool isRegistrationCompleted)
        {
            Id = new AggregateId(id);
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Password = password;
            Address = address;
            IsRegistrationCompleted = isRegistrationCompleted;
        }

        public User(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;

            AddEvent(new UserCreated(this));
        }

        public void UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidFirstNameValueException();

            FirstName = firstName;

            AddEvent(new FirstNameChanged(this, firstName));
        }

        public void UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidLastNameValueException();

            LastName = lastName;
         
            AddEvent(new FirstNameChanged(this, lastName));
        }

        public void CompleteRegistration()
        {
            IsRegistrationCompleted = true;

            AddEvent(new RegistrationCompleted(this));
        }

        public void UpdateAddress(string street, string city, string zipcode, string country)
        {
            Address = Address.Create(street, city, zipcode, country);

            AddEvent(new AddressChanged(this, Address));
        }
    }
}