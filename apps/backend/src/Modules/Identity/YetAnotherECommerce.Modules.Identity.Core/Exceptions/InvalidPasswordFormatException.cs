using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class InvalidPasswordFormatException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_password_format";

        public InvalidPasswordFormatException() : base("Given password has invalid format.")
        {

        }
    }
}