using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class SomeOfOrderedProductsAreNotAvailableException()
    : YetAnotherECommerceException("Some of ordered products are not available.")
{
    public override string ErrorCode => "products_not_available";
}