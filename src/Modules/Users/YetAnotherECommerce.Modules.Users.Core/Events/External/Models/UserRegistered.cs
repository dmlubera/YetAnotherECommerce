using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Models
{
    public record UserRegistered(
        Guid Id,
        string Email) : IEvent;
}