using System;
using System.Collections.Generic;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; private set; }
        
        protected Email() { }

        public Email(string value)
            => Value = value;

        public static Email Create(string email)
        {
            if (!HasValidFormat(email))
                throw new InvalidEmailFormatException();

            return new Email(email);
        }

        public static bool HasValidFormat(string email)
            => !string.IsNullOrWhiteSpace(email);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email value)
            => value.Value;

        public static implicit operator Email(string value)
            => Create(value);
    }
}