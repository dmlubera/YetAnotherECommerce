using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Catalog.Core.Events.External.Models;

public record OrderCanceled(
    Guid OrderId,
    IDictionary<Guid, int> Products) : IEvent;