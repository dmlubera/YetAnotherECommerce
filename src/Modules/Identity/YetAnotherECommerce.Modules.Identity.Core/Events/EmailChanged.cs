using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Core.Events
{
    public record EmailChanged(
        Guid UserId,
        string Email) : IEvent;
}