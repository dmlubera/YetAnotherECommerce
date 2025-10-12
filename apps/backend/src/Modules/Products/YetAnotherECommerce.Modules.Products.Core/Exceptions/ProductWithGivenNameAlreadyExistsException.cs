using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class ProductWithGivenNameAlreadyExistsException : YetAnotherECommerceException
    {
        public override string ErrorCode => "product_already_exists";

        public ProductWithGivenNameAlreadyExistsException() : base("Product with given name already exists.")
        {

        }
    }
}