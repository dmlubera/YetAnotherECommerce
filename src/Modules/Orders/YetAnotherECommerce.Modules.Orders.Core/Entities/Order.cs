using System;
using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities
{
    public class Order : AggregateRoot
    {
        private readonly List<OrderItem> _orderItems;
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

            AddEvent(new OrderCreated(this));
        }

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
            AddEvent(new OrderStatusChanged(this, Status));
        }
    }
}