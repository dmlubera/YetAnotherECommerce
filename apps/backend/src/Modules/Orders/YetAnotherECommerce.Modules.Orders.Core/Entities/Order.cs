using System;
using System.Collections.Generic;
using System.Linq;
using YetAnotherECommerce.Modules.Orders.Core.DomainEvents;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities;

public class Order : AggregateRoot, IAuditable
{
    private readonly List<OrderItem> _orderItems;
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTime? CreatedAt { get; }
    public DateTime? LastUpdatedAt { get; private set; }
    public decimal TotalPrice => _orderItems.Sum(x => x.Quantity * x.UnitPrice);

    protected Order() { }

    public Order(Guid id, Guid customerId, OrderStatus status, List<OrderItem> orderItems, DateTime? createdAt, DateTime? lastUpdatedAt)
    {
        Id = id;
        CustomerId = customerId;
        Status = status;
        _orderItems = orderItems;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
    }

    public Order(Guid customerId, List<OrderItem> orderItems)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        _orderItems = orderItems;
        Status = OrderStatus.Created;
        CreatedAt = DateTime.UtcNow;

        AddEvent(new OrderCreated(this));
    }

    public void AcceptOrder()
    {
        if (Status != OrderStatus.Created)
            throw new AcceptationNotAllowedException(Status);

        Status = OrderStatus.Accepted;
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new OrderAccepted(this, Status));
    }

    public void CancelOrder()
    {
        if (Status != OrderStatus.Created && Status != OrderStatus.Accepted)
            throw new CancellationNotAllowedException(Status);

        Status = OrderStatus.Canceled;
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new OrderCanceled(this, Status));
    }

    public void CompleteOrder()
    {
        if (Status != OrderStatus.Accepted)
            throw new CompletionNotAllowedException(Status);

        Status = OrderStatus.Completed;
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new OrderCompleted(this, Status));
    }

    public void RejectOrder()
    {
        if (Status != OrderStatus.Accepted)
            throw new RejectionNotAllowedException(Status);

        Status = OrderStatus.Rejected;
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new OrderRejected(this, Status));
    }
}