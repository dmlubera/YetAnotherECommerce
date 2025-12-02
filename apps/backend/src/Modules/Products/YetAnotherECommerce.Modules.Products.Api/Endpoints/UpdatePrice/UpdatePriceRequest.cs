using System;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.UpdatePrice;

public record UpdatePriceRequest(Guid ProductId, decimal Price);