using System.Collections.Generic;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Password : ValueObject
    {
        public string Hash { get; private set; }

        protected Password() { }

        public Password(string hash) => Hash = hash;

        public static Password Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidPasswordFormatException();

            return new Password(BCrypt.Net.BCrypt.EnhancedHashPassword(password));
        }

        public bool IsValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new InvalidPasswordFormatException();

            return BCrypt.Net.BCrypt.EnhancedVerify(password, Hash);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}