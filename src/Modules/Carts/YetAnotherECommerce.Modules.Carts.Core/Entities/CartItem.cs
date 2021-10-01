using System;

namespace YetAnotherECommerce.Modules.Carts.Core.Entities
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public CartItem(Guid productId, string name, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void IncreaseQuantity(int quantity)
            => Quantity += quantity;
    }
}