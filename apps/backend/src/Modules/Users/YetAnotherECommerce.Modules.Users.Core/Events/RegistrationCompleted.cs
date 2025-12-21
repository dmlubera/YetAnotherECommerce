using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events;

public record RegistrationCompleted(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Email,
    string Address) : IEvent;