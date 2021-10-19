using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Messages.Events
{
    public class OrderPlaced : IEvent
    {
        public Guid CustomerId { get; set; }
        public IDictionary<Guid, int> Products { get; set; }

        public OrderPlaced(Guid customerId, IDictionary<Guid, int> products)
        {
            CustomerId = customerId;
            Products = products;
        }
    }
}