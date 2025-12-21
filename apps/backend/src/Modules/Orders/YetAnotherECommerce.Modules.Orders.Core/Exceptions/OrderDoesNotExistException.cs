using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions;

public class OrderDoesNotExistException(Guid orderId)
    : YetAnotherECommerceException($"Order with ID: {orderId} does not exist.")
{
    public override string ErrorCode => "order_not_exists";
}