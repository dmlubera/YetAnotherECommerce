using System;
using YetAnotherECommerce.Modules.Products.Core.DomainEvents;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.Entitites
{
    public class Product : AggregateRoot
    {
        public Name Name { get; private set; }
        public string Description { get; private set; }
        public Price Price { get; private set; }
        public Quantity Quantity { get; private set; }

        protected Product() { }

        public Product(Guid id, string name, string description, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public Product(string name, string description, decimal price, int quantity)
        {
            Id = Guid.NewGuid();
            Name = Name.Create(name);
            Description = description;
            Price = Price.Create(price);
            Quantity = Quantity.Create(quantity);

            AddEvent(new ProductCreated(this));
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = Quantity.Create(quantity);

            AddEvent(new QuantityUpdated(this, quantity));
        }
    }
}