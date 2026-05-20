using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Catalog.Core.Events;

public record OrderRejected(
    Guid OrderId) : IEvent;