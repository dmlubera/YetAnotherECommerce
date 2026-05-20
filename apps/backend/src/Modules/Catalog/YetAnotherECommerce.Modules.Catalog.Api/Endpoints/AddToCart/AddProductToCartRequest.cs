using System;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.AddToCart;

public record AddProductToCartRequest(Guid ProductId, int Quantity);