namespace YetAnotherECommerce.Modules.Identity.Core.ValueObjects
{
    public class Password
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        protected Password() { }

        protected Password(string hash, string salt)
            => (Hash, Salt) = (hash, salt);

        public static Password Create(string hash, string salt)
            => new Password(hash, salt);
    }
}