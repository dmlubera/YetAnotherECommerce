using System;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities;

public class Customer(Guid id, string firstName, string lastName, string email, string address)
{
    public Guid Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public string Address { get; set; } = address;
}