using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Carts.Core.Exceptions
{
    public class CannotOrderProductInZeroQuantityException : YetAnotherECommerceException
    {
        public override string ErrorCode => "cannot_order_product_in_zero_quantity";

        public CannotOrderProductInZeroQuantityException()
            : base("Cannot order a product in zero quantity.")
        {

        }
    }
}