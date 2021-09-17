using System;

namespace YetAnotherECommerce.Modules.Products.Core.Entitites
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public Product()
        {
            Id = Guid.NewGuid();
        }
    }
}