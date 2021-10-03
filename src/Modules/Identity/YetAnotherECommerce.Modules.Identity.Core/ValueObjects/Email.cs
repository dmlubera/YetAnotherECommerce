using System;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Value { get; private set; }
        
        protected Email() { }

        protected Email(string value)
            => Value = value;

        public bool Equals(Email other)
            => other is Email && Value == other.Value;

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidEmailFormatException();

            return new Email(email);
        }

        public static implicit operator string(Email value)
            => value.Value;

        public static implicit operator Email(string value)
            => Create(value);
    }
}