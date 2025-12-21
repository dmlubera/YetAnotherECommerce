using System.Collections.Generic;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.ValueObjects;

public class Quantity : ValueObject
{
    public int Value { get; private set; }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Quantity Create(int value)
    {
        if (value < 0)
            throw new InvalidQuantityValueException();

        return new Quantity(value);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(int quntity) => new Quantity(quntity);
}