using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class RoleNotExistException : YetAnotherECommerceException
    {
        public override string ErrorCode => "role_not_exist";

        public RoleNotExistException(string role)
            : base($"Specified role: {role} does not exist.")
        {

        }
    }
}