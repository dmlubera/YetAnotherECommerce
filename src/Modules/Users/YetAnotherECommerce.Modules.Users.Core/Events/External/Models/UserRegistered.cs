using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Models
{
    public class UserRegistered : IEvent
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }

        public UserRegistered(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}