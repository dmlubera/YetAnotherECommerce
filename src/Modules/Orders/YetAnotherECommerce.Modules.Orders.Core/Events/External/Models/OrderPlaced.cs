using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Models
{
    public record OrderPlaced(
        Guid CustomerId,
        IList<ProductDto> Products) : IEvent;
}