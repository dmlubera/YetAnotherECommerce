using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Models
{
    public record OrderRejected(
        Guid OrderId) : IEvent;
}