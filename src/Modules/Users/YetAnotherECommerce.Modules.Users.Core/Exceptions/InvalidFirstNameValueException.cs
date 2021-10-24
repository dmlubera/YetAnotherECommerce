using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions
{
    public class InvalidFirstNameValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_firstname";

        public InvalidFirstNameValueException()
            : base("Firstname has invalid value.")
        {

        }
    }
}