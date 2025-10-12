using System;

namespace YetAnotherECommerce.Modules.Carts.Core.Events
{
    public record ProductDto(
        Guid ProductId,
        string Name,
        decimal UnitPrice,
        int Quantity);
}