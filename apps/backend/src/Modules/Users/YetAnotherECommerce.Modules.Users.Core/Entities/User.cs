using System;
using YetAnotherECommerce.Modules.Users.Core.DomainEvents;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.Entities;

public class User : AggregateRoot, IAuditable
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public string Email { get; private set; }
    public Address Address { get; private set; }
    public bool IsRegistrationCompleted { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }

    protected User() { }

    public User(Guid id, string firstName, string lastName, string email,
        Address address, bool isRegistrationCompleted, DateTime? createdAt, DateTime? lastUpdatedAt)
    {
        Id = new AggregateId(id);
        LastName = lastName;
        FirstName = firstName;
        Email = email;
        Address = address;
        IsRegistrationCompleted = isRegistrationCompleted;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
    }

    public User(Guid id, string email)
    {
        Id = id;
        Email = email;
        CreatedAt = DateTime.UtcNow;

        AddEvent(new UserCreated(this));
    }

    public void UpdateFirstName(string firstName)
    {
        FirstName = FirstName.Create(firstName);
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new FirstNameChanged(this, firstName));
    }

    public void UpdateLastName(string lastName)
    {
        LastName = LastName.Create(lastName);
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new LastNameChanged(this, lastName));
    }

    public void CompleteRegistration()
    {
        IsRegistrationCompleted = true;
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new RegistrationCompleted(this));
    }

    public void UpdateAddress(string street, string city, string zipcode, string country)
    {
        Address = Address.Create(street, city, zipcode, country);
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new AddressChanged(this, Address));
    }
}