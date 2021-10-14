using System;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.ValueObjects
{
    public class Quantity : IEquatable<Quantity>
    {
        public int Value { get; }

        public Quantity(int value)
        {
            Value = value;
        }
        
        public static Quantity Create(int value)
        {
            if (value < 0)
                throw new InvalidQuantityValueException();

            return new Quantity(value);
        }

        public bool Equals(Quantity other)
        {
            return Value == other.Value;
        }

        public static implicit operator int(Quantity quantity) => quantity.Value;

        public static implicit operator Quantity(int quntity) => new Quantity(quntity);
    }
}