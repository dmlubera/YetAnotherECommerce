﻿using System;
using System.Collections.Generic;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Order(Guid id, Guid customerId, OrderStatus status, List<OrderItem> orderItems)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            _orderItems = orderItems;
        }

        public Order(Guid customerId, List<OrderItem> orderItems)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            _orderItems = orderItems;
            Status = OrderStatus.Created;
        }

        public void UpdateStatus(OrderStatus status)
            => Status = status;
    }
}