using System;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.AddToCart;

public record AddProductToCartRequest(Guid ProductId, int Quantity);