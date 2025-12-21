using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions;

public class CancellationNotAllowedException(OrderStatus status) : YetAnotherECommerceException(
    $"Cancelation can be performed only for orders in statuses {OrderStatus.Accepted} or {OrderStatus.Created}. Actual status is {status}")
{
    public override string ErrorCode => "cancelation_denied";
}