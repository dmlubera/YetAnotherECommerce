using System.Collections.Generic;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.ValueObjects;

public class Name : ValueObject
{
    public string Value { get; private set; }

    private Name(string value)
        => Value = value;

    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidProductNameException();

        return new(value);
    }

    public static implicit operator string(Name name) => name.Value;

    public static implicit operator Name(string name) => new(name);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}