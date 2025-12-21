using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions;

public class RejectionNotAllowedException(OrderStatus status) : YetAnotherECommerceException(
    $"Rejection can be performed only for orders in status {OrderStatus.Accepted}. Actual status is {status}")
{
    public override string ErrorCode => "rejection_denied";
}