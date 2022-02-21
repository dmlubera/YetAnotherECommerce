﻿using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events
{
    public record ProductAddedToCart(
        Guid CustomerdId,
        Guid ProductId,
        string Name,
        decimal UnitPrice,
        int Quantity) : IEvent;
}