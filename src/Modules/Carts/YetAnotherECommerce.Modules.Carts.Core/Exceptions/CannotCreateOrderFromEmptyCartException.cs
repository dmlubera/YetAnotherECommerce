using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Carts.Core.Exceptions
{
    public class CannotCreateOrderFromEmptyCartException : YetAnotherECommerceException
    {
        public override string ErrorCode => "cannot_create_order_from_empty_cart";

        public CannotCreateOrderFromEmptyCartException()
            : base("Cannot create an order from an empty cart.")
        {

        }
    }
}