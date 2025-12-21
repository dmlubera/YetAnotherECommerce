using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class InvalidPriceException() : YetAnotherECommerceException("Price has invalid value.")
{
    public override string ErrorCode => "invalid_price";
}