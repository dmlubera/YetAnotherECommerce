using System;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities;

public class OrderItem(Guid id, Guid productId, string name, decimal unitPrice, int quantity)
{
    public Guid Id { get; set; } = id;
    public Guid ProductId { get; set; } = productId;
    public string Name { get; set; } = name;
    public decimal UnitPrice { get; set; } = unitPrice;
    public int Quantity { get; set; } = quantity;

    public OrderItem(Guid productId, string name, decimal unitPrice, int quantity) : this(Guid.NewGuid(), productId, name, unitPrice, quantity)
    {
    }
}