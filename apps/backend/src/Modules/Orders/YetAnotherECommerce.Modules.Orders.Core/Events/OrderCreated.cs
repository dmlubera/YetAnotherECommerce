using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events
{
    public record OrderCreated(
        Guid OrderId,
        IDictionary<Guid, int> Products) : IEvent;
}