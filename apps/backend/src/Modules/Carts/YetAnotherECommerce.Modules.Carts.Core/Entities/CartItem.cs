using System;

namespace YetAnotherECommerce.Modules.Carts.Core.Entities;

public class CartItem(Guid productId, string name, int quantity, decimal unitPrice)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; } = productId;
    public string Name { get; private set; } = name;
    public int Quantity { get; private set; } = quantity;
    public decimal UnitPrice { get; private set; } = unitPrice;
    public decimal TotalPrice => UnitPrice * Quantity;

    public void IncreaseQuantity(int quantity)
        => Quantity += quantity;

    public void UpdatePrice(decimal price)
        => UnitPrice = price;
}