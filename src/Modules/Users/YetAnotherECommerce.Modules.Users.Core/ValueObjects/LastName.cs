using System.Collections.Generic;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.ValueObjects
{
    public class LastName : ValueObject
    {
        public string Value { get; private set; }

        private LastName(string value)
            => Value = value;

        public static LastName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidLastNameValueException();

            return new(value);
        }

        public static implicit operator string(LastName firstName) => firstName.Value;

        public static implicit operator LastName(string firstName) => new(firstName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}