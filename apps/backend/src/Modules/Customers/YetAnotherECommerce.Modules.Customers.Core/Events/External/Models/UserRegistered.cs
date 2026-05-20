using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Customers.Core.Events.External.Models;

public record UserRegistered(
    Guid Id,
    string Email) : IEvent;