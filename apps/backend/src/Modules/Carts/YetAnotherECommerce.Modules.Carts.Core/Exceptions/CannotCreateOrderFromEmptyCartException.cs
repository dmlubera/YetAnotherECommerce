using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Carts.Core.Exceptions;

public class CannotCreateOrderFromEmptyCartException()
    : YetAnotherECommerceException("Cannot create an order from an empty cart.")
{
    public override string ErrorCode => "cannot_create_order_from_empty_cart";
}