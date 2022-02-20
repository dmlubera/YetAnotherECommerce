using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Events.External.Models
{
    public record ProductAddedToCart(
        Guid CustomerId,
        Guid ProductId,
        string Name,
        decimal UnitPrice,
        int Quantity) : IEvent;
}