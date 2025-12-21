using System;
using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Models;

public record OrderRevoked(
    Guid OrderId,
    IDictionary<Guid, int> Products) : IEvent;