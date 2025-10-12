using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidStreetValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_street_value";

        public InvalidStreetValueException()
            : base("Street has invalid value.")
        {

        }
    }
}