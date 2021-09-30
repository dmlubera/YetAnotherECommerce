using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions
{
    public class ProductDoesNotExistException : YetAnotherECommerceException
    {
        public override string ErrorCode => "product_does_not_exist";

        public ProductDoesNotExistException(Guid id)
            : base($"Product with ID: {id} does not exist.")
        {

        }
    }
}