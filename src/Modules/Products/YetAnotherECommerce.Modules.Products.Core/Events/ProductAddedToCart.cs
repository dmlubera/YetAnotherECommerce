using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events
{
    public class ProductAddedToCart : IEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public ProductAddedToCart(Guid customerId, Guid productId, string name, decimal unitPrice, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}