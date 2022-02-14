using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class InvalidPasswordHashException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_password_hash";

        public InvalidPasswordHashException()
            : base("Hash cannot be null or whitespace.")
        {

        }
    }
}