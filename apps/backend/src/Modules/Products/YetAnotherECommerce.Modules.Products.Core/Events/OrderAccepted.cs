using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events
{
    public record OrderAccepted(
        Guid OrderId) : IEvent;
}