using System;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Products.Core.Entitites
{
    public class Product : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Price Price { get; private set; }
        public Quantity Quantity { get; private set; }
        
        public Product(Guid id, string name, string description, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = new Price(price);
            Quantity = new Quantity(quantity);
        }

        public Product(string name, string description, decimal price, int quantity)
        {
            Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidProductNameException();
            Name = name;
            Description = description;
            Price = Price.Create(price);
            Quantity = Quantity.Create(quantity);
        }
    }
}