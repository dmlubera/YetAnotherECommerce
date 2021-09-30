using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Messages.Events
{
    public class ProductAddedToCart : IEvent
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public ProductAddedToCart(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}