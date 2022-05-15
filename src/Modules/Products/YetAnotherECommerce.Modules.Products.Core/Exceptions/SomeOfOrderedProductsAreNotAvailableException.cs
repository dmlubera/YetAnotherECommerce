using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class SomeOfOrderedProductsAreNotAvailableException : YetAnotherECommerceException
    {
        public override string ErrorCode => "products_not_available";

        public SomeOfOrderedProductsAreNotAvailableException()
            : base("Some of ordered products are not available.")
        {

        }
    }
}