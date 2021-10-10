using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidQuantityException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_quantity";

        public InvalidQuantityException()
            : base("Invalid quantity value.")
        {

        }
    }
}