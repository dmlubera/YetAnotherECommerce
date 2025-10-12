using System;

namespace YetAnotherECommerce.Modules.Products.Api.Models.Requests
{
    public record UpdatePriceRequest(Guid ProductId, decimal Price);
}