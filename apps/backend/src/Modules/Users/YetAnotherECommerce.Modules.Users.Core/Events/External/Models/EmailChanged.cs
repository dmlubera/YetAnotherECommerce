using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Models
{
    public record EmailChanged(
        Guid UserId,
        string Email) : IEvent;
}