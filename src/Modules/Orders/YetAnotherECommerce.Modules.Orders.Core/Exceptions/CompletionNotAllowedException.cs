using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions
{
    public class CompletionNotAllowedException : YetAnotherECommerceException
    {
        public override string ErrorCode => "completation_denied";

        public CompletionNotAllowedException(OrderStatus status)
            : base($"Completion can be performed only for orders in status {OrderStatus.Accepted}. Actual status is {status}")
        {

        }
    }
}
