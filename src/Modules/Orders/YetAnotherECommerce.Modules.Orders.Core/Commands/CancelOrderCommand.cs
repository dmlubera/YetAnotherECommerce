using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class CancelOrderCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }

        public CancelOrderCommand(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}