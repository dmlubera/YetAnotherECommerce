using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Messages.Events
{
    public class OrderCanceled : IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; set; }

        public OrderCanceled(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}