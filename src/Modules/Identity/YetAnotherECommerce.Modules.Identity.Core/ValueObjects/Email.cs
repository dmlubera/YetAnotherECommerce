using System;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Value { get; private set; }
        
        protected Email() { }

        public Email(string value)
            => Value = value;

        public bool Equals(Email other)
            => other is Email && Value == other.Value;

        public static bool HasValidFormat(string email)
            => !string.IsNullOrWhiteSpace(email);

        public static Email Create(string email)
        {
            if (!HasValidFormat(email))
                throw new InvalidEmailFormatException();

            return new Email(email);
        }

        public static implicit operator string(Email value)
            => value.Value;

        public static implicit operator Email(string value)
            => Create(value);
    }
}