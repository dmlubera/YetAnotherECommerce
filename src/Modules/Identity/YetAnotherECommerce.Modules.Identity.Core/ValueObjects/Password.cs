using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Password : ValueObject
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        protected Password() { }

        public Password(string hash, string salt)
            => (Hash, Salt) = (hash, salt);

        public static Password Create(string hash, string salt)
            => new Password(hash, salt);

        public static bool HasValidFormat(string password)
            => !string.IsNullOrWhiteSpace(password);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }
    }
}