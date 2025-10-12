using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidCityValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_city_value";

        public InvalidCityValueException()
            : base("City has invalid value.")
        {

        }
    }
}