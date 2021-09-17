using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidProductNameException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_product_name";

        public InvalidProductNameException() : base("Product name has invalid name.")
        {

        }
    }
}