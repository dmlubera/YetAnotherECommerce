namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.AddProduct;

public record AddProductRequest(string Name, string Description, decimal Price, int Quantity);