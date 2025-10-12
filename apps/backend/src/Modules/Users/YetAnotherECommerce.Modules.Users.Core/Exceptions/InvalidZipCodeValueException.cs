using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidZipCodeValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_zipcode_value";

        public InvalidZipCodeValueException()
            : base("Zip code has invalid value.")
        {

        }
    }
}