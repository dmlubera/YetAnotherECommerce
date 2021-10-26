using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class CompleteOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }

        public CompleteOrderCommand(Guid orderId)
            => OrderId = orderId;
    }
}