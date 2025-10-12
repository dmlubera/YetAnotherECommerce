using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Events
{
    public record OrderPlaced(
        Guid CustomerId,
        IList<ProductDto> Products) : IEvent;
}