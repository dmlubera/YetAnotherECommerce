using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class InvalidProductNameException() : YetAnotherECommerceException("Product name has invalid name.")
{
    public override string ErrorCode => "invalid_product_name";
}