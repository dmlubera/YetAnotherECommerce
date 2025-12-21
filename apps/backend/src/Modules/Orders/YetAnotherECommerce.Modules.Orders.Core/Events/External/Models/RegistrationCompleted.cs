using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;

public record RegistrationCompleted(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Email,
    string Address) : IEvent;