using System;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.UpdatePrice;

public record UpdatePriceRequest(Guid ProductId, decimal Price);