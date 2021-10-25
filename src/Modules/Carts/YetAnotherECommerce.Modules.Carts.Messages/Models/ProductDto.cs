using System;

namespace YetAnotherECommerce.Modules.Carts.Messages.Model
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quanitity { get; set; }

        public ProductDto(Guid productId, string name, decimal unitPrice, int quanitity)
        {
            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Quanitity = quanitity;
        }
    }
}