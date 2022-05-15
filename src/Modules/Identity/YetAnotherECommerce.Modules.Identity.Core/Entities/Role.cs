namespace YetAnotherECommerce.Modules.Identity.Core.Entities
{
    public static class Role
    {
        public const string Admin = "admin";
        public const string Customer = "customer";

        public static bool IsValid(string role)
        {
            if(string.IsNullOrWhiteSpace(role))
                return false;

            role = role.ToLowerInvariant();

            return role == Admin || role == Customer;
        }
    }
}