using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Events
{
    public class OrderPlaced : IEvent
    {
        public Guid CustomerId { get; set; }
        public IList<ProductDto> Products { get; set; }

        public OrderPlaced(Guid customerId, IList<ProductDto> products)
        {
            CustomerId = customerId;
            Products = products;
        }
    }
}