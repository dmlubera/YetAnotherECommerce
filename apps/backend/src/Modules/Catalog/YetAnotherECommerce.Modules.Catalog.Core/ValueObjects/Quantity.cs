using System.Collections.Generic;
using YetAnotherECommerce.Modules.Catalog.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Catalog.Core.ValueObjects;

public class Quantity : ValueObject
{
    public int Value { get; }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Quantity Create(int value)
    {
        return value < 0 ? throw new InvalidQuantityValueException() : new Quantity(value);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(int quntity) => new(quntity);
}