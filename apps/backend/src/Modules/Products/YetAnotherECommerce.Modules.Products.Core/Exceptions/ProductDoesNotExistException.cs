using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Exceptions;

public class ProductDoesNotExistException(Guid id)
    : YetAnotherECommerceException($"Product with ID: {id} does not exist.")
{
    public override string ErrorCode => "product_does_not_exist";
}