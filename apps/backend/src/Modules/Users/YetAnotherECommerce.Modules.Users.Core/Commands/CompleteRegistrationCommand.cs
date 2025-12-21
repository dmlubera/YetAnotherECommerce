using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Users.Core.Commands;

public class CompleteRegistrationCommand(
    Guid userId,
    string firstName,
    string lastName,
    string street,
    string city,
    string zipCode,
    string country)
    : ICommand
{
    public Guid UserId { get; set; } = userId;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Street { get; set; } = street;
    public string City { get; set; } = city;
    public string ZipCode { get; set; } = zipCode;
    public string Country { get; set; } = country;
}