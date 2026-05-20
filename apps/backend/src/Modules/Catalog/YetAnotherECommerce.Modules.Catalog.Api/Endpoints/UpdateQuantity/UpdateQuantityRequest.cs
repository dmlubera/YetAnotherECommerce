using System;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.UpdateQuantity;

public record UpdateQuantityRequest(Guid ProductId, int Quantity);