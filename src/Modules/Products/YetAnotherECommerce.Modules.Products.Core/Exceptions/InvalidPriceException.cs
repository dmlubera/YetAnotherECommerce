using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidPriceException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_price";

        public InvalidPriceException() : base("Price has invalid value.")
        {

        }
    }
}