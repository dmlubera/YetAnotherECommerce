using System;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.UpdateQuantity;

public record UpdateQuantityRequest(Guid ProductId, int Quantity);