using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Core.Events
{
    public class UserRegistered : IEvent
    {
        public Guid Id { get; init; }
        public string Email { get; init; }

        public UserRegistered(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}