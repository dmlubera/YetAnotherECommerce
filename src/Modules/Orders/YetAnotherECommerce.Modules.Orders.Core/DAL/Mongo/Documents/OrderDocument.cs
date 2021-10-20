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
    }
}