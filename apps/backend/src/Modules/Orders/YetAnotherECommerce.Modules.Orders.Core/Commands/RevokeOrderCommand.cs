using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class RevokeOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }

        public RevokeOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}