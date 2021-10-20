using System;

namespace YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Documents
{
    public class OrderItemDocument
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}