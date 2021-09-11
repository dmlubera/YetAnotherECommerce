using System;

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
            => new Email(email);
    }
}