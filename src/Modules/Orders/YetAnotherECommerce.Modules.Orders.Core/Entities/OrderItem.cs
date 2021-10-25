using System;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public OrderItem(Guid id, Guid productId, string name, decimal unitPrice, int quantity)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public OrderItem(Guid productId, string name, decimal unitPrice, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}