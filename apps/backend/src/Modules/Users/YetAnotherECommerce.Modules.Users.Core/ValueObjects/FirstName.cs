using System.Collections.Generic;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.ValueObjects;

public class FirstName : ValueObject
{
    public string Value { get; }

    private FirstName(string value)
        => Value = value;

    public static FirstName Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? throw new InvalidFirstNameValueException() : new FirstName(value);
    }

    public static implicit operator string(FirstName firstName) => firstName?.Value;

    public static implicit operator FirstName(string firstName) => new(firstName);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}