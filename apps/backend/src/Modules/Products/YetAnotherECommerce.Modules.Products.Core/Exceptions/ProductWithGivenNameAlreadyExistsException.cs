using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class ProductWithGivenNameAlreadyExistsException()
    : YetAnotherECommerceException("Product with given name already exists.")
{
    public override string ErrorCode => "product_already_exists";
}