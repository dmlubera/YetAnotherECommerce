using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions
{
    public class NoSuchOrderExistsForCustomerException : YetAnotherECommerceException
    {
        public override string ErrorCode => "no_such_order_exists_for_customer";

        public NoSuchOrderExistsForCustomerException(Guid orderId, Guid customerId)
            : base($"Order with ID: {orderId} does not exist for customer with ID: {customerId}.")
        {

        }
    }
}