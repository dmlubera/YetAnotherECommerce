using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace YetAnotherECommerce.Modules.Identity.Core.Helpers
{
    public class Encrypter : IEncrypter
    {
        public string GetHash(string value, string salt)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

        public string GetSalt()
        {
            var bytes = new byte[128 / 8];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        public bool IsEqual(string originalHash, string originalSalt, string valueToCompare)
            => originalHash == GetHash(valueToCompare, originalSalt);
    }
}