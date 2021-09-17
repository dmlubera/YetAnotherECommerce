using System;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Products.Core.Entitites
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public Product(string name, string description, decimal price)
        {
            Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidProductNameException();
            Name = name;
            Description = description;
            if (price <= 0)
                throw new InvalidPriceException();
            Price = price;
        }
    }
}