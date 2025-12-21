using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class InvalidQuantityValueException() : YetAnotherECommerceException("Invalid quantity value.")
{
    public override string ErrorCode => "invalid_quantity";
}