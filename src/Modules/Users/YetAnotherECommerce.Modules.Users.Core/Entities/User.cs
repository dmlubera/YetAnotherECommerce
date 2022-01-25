﻿using System;
using YetAnotherECommerce.Modules.Users.Core.DomainEvents;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.Entities
{
    public class User : AggregateRoot
    {
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
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
            FirstName = FirstName.Create(firstName);

            AddEvent(new FirstNameChanged(this, firstName));
        }

        public void UpdateLastName(string lastName)
        {
            LastName = LastName.Create(lastName);
         
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