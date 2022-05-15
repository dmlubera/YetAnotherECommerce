using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidCountryValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_country_value";

        public InvalidCountryValueException()
            : base("Country has invalid value.")
        {

        }
    }
}