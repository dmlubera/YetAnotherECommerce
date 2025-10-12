using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions
{
    public class RejectionNotAllowedException : YetAnotherECommerceException
    {
        public override string ErrorCode => "rejection_denied";

        public RejectionNotAllowedException(OrderStatus status)
            : base($"Rejection can be performed only for orders in status {OrderStatus.Accepted}. Actual status is {status}")
        {

        }
    }
}