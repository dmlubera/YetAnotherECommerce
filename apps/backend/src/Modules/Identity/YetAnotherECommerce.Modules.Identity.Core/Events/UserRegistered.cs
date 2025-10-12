using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Core.Events
{
    public record UserRegistered(
        Guid Id,
        string Email) : IEvent;
}