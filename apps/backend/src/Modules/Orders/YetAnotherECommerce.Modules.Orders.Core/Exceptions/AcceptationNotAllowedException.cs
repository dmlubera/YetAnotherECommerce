using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions;

public class AcceptationNotAllowedException(OrderStatus status) : YetAnotherECommerceException(
    $"Acceptation can be performed only for orders in status {OrderStatus.Created}. Actual status is {status}")
{
    public override string ErrorCode => "acceptation_denied";
}