using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class InvalidEmailFormatException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_email_format";

        public InvalidEmailFormatException() : base("Email has invalid format.")
        {

        }
    }
}