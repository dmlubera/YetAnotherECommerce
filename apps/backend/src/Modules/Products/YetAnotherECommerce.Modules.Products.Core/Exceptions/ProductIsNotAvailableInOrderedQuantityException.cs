using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class ProductIsNotAvailableInOrderedQuantityException()
    : YetAnotherECommerceException("Order is not available in ordered quantity.")
{
    public override string ErrorCode => "product_not_available_in_ordered_quantity";
}