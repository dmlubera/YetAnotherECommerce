using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events
{
    public class OrderRejected : IEvent
    {
        public Guid OrderId { get; set; }
        public DateTime DateTime { get; set; }

        public OrderRejected(Guid orderId)
        {
            OrderId = orderId;
            DateTime = DateTime.UtcNow;
        }
    }
}