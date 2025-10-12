using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class InvalidQuantityValueException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_quantity";

        public InvalidQuantityValueException()
            : base("Invalid quantity value.")
        {

        }
    }
}