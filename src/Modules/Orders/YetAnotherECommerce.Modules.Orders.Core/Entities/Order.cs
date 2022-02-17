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

        public void AcceptOrder()
        {
            Status = OrderStatus.Accepted;
            AddEvent(new OrderAccepted(this, Status));
        }

        public void CancelOrder()
        {
            Status = OrderStatus.Canceled;
            AddEvent(new OrderCanceled(this, Status));
        }

        public void CompleteOrder()
        {
            Status = OrderStatus.Completed;
            AddEvent(new OrderCompleted(this, Status));
        }

        public void RejectOrder()
        {
            Status = OrderStatus.Rejected;
            AddEvent(new OrderRejected(this, Status));
        }
    }
}