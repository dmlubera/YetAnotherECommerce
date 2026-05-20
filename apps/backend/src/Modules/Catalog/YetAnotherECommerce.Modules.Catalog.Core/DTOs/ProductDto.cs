using System;

namespace YetAnotherECommerce.Modules.Catalog.Core.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}