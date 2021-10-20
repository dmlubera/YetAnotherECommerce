using System;
using System.Collections.Generic;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Status { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Order(Guid customerId, List<OrderItem> orderItems)
        {
            CustomerId = customerId;
            _orderItems = orderItems;
            Status = "Created";
        }
    }
}