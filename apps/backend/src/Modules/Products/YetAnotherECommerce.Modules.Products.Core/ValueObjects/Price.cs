using System.Collections.Generic;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.ValueObjects;

public class Price : ValueObject
{
    public decimal Value { get; private set; }

    private Price(decimal value)
    {
        Value = value;
    }

    public static Price Create(decimal value)
    {
        if (value < decimal.Zero)
            throw new InvalidPriceException();

        return new Price(value);
    }
        
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Price price) => price.Value;

    public static implicit operator Price(decimal price) => new Price(price);
}