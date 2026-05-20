using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Catalog.Core.Exceptions;

public class InvalidProductNameException() : YetAnotherECommerceException("Product name has invalid name.")
{
    public override string ErrorCode => "invalid_product_name";
}