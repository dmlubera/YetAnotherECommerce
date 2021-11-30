using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Models
{
    public class OrderAccepted : IEvent
    {
        public Guid OrderId { get; set; }
        public DateTime DateTime { get; set; }

        public OrderAccepted(Guid orderId)
        {
            OrderId = orderId;
            DateTime = DateTime.UtcNow;
        }
    }
}