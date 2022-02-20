using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions
{
    public class CancellationNotAllowedException : YetAnotherECommerceException
    {
        public override string ErrorCode => "cancelation_denied";

        public CancellationNotAllowedException(OrderStatus status)
            : base($"Cancelation can be performed only for orders in statuses {OrderStatus.Accepted} or {OrderStatus.Created}. Actual status is {status}")
        {

        }
    }
}
