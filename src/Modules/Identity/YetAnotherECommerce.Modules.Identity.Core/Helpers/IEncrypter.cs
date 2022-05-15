namespace YetAnotherECommerce.Modules.Identity.Core.Helpers
{
    public interface IEncrypter
    {
        string GetSalt();
        string GetHash(string value, string salt);
        bool IsEqual(string originalHash, string originalSalt, string valueToCompare);
    }
}