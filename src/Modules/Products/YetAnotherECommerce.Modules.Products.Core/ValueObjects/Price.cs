using System;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.ValueObjects
{
    public class Price : IEquatable<Price>
    {
        public decimal Value { get; set; }

        public Price(decimal value)
        {
            Value = value;
        }

        public static Price Create(decimal value)
        {
            if (value < decimal.Zero)
                throw new InvalidPriceException();

            return new Price(value);
        }

        bool IEquatable<Price>.Equals(Price other)
        {
            return Value == other.Value;
        }

        public static implicit operator decimal(Price price) => price.Value;

        public static implicit operator Price(decimal price) => new Price(price);
    }
}
