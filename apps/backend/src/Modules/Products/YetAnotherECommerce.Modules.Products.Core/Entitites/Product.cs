using System;
using YetAnotherECommerce.Modules.Products.Core.DomainEvents;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.Entitites;

public class Product : AggregateRoot, IAuditable
{
    public Name Name { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public Quantity Quantity { get; private set; }
    public DateTime? CreatedAt { get; }
    public DateTime? LastUpdatedAt { get; private set; }

    protected Product() { }

    public Product(Guid id, string name, string description, decimal price, int quantity, DateTime? createdAt, DateTime? lastUpdatedAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
    }

    public Product(string name, string description, decimal price, int quantity)
    {
        Id = Guid.NewGuid();
        Name = Name.Create(name);
        Description = description;
        Price = Price.Create(price);
        Quantity = Quantity.Create(quantity);
        CreatedAt = DateTime.UtcNow;

        AddEvent(new ProductCreated(this));
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = Quantity.Create(quantity);
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new QuantityUpdated(this, quantity));
    }

    public void UpdatePrice(decimal price)
    {
        Price = Price.Create(price);
        LastUpdatedAt = DateTime.UtcNow;

        AddEvent(new PriceUpdated(this, price));
    }
}