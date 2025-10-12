using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions
{
    public class InvalidLastNameValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_lastname";

        public InvalidLastNameValueException()
            : base("Lastname has invalid value.")
        {

        }
    }
}
