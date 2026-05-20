using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Catalog.Core.Events;

public record PriceUpdated(Guid PriceId, decimal Price) : IEvent;