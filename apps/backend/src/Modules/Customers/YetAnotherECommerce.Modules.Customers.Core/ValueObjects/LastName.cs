using System.Collections.Generic;
using YetAnotherECommerce.Modules.Customers.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Customers.Core.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }

    private LastName(string value)
        => Value = value;

    public static LastName Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? throw new InvalidLastNameValueException() : new LastName(value);
    }

    public static implicit operator string(LastName lastName) => lastName?.Value;

    public static implicit operator LastName(string lastName) => new(lastName);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}