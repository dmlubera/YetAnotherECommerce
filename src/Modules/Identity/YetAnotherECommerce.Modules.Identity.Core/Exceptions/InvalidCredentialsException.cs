using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class InvalidCredentialsException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_credentials";

        public InvalidCredentialsException()
            : base("Given credentials are invalid.")
        {

        }
    }
}