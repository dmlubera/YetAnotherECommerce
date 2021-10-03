using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Password
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        protected Password() { }

        protected Password(string hash, string salt)
            => (Hash, Salt) = (hash, salt);

        public static Password Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidPasswordFormatException();

            var salt = GenerateSalt();
            var hash = GenerateHash(password, salt);

            return new Password(hash, salt);
        }

        private static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        private static string GenerateHash(string password, string salt)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

        public static bool IsValid(Password password, string givenPassword)
            => password.Hash == GenerateHash(givenPassword, password.Salt);
    }
}