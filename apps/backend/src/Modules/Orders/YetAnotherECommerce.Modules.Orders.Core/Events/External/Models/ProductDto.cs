using System;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;

public record ProductDto(
    Guid ProductId,
    string Name,
    decimal UnitPrice,
    int Quantity);