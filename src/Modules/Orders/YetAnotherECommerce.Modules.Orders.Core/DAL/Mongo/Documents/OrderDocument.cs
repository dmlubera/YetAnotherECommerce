using System;
using System.Collections.Generic;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Documents
{
    public class OrderDocument
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; }
        public List<OrderItemDocument> OrderItems { get; set; }

        public OrderDocument(Guid id, Guid customerId, string status, List<OrderItemDocument> orderItems)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            OrderItems = orderItems;
        }
    }
}