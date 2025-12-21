using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions;

public class CustomerWithGivenIdDoesNotExistsException(Guid id)
    : YetAnotherECommerceException($"Customer with ID: {id} does not exists.")
{
    public override string ErrorCode => "customer_not_exists";
}