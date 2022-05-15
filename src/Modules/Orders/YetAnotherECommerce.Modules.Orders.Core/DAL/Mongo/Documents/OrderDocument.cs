using System;
using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Documents
{
    public class OrderDocument
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDocument> OrderItems { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public OrderDocument(Guid id, Guid customerId, OrderStatus status,
            List<OrderItemDocument> orderItems, DateTime? createdAt, DateTime? lastUpdatedAt)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            OrderItems = orderItems;
            CreatedAt = createdAt;
            LastUpdatedAt = lastUpdatedAt;
        }
    }
}