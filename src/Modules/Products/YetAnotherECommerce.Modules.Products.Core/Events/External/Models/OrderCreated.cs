using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Models
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; set; }

        public OrderCreated(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}