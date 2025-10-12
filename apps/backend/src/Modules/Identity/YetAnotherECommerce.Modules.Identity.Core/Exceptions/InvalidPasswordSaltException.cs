using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class InvalidPasswordSaltException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_password_salt";

        public InvalidPasswordSaltException()
            : base("Salt cannot be null or whitespace.")
        {

        }
    }
}