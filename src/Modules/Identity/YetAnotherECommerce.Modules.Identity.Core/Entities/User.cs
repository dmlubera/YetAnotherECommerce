using System;
using YetAnotherECommerce.Modules.Identity.Core.DomainEvents;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Identity.Core.Entities
{
    public class User : AggregateRoot
    {
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected User() { }

        public User(Guid id, string email, string hash, string salt, string role, DateTime createdAt)
        {
            Id = id;
            Email = new Email(email);
            Password = new Password(hash, salt);
            Role = role;
            CreatedAt = createdAt;
        }

        public User(string email, string password, string role)
        {
            Id = new AggregateId();
            Email = Email.Create(email);
            Password = Password.Create(password);
            SetRole(role);
            CreatedAt = DateTime.UtcNow;

            AddEvent(new UserRegistered(this));
        }

        public void ChangeEmail(string email)
        {
            Email = Email.Create(email);

            AddEvent(new EmailChanged(this, Email));
        }

        public void ChangePassword(string password)
        {
            Password = Password.Create(password);
         
            AddEvent(new PasswordChanged(this, Password));
        }

        private void SetRole(string role)
        {
            if (!Enum.IsDefined(typeof(AllowedRoles), role.ToLower()))
                throw new RoleNotExistException(role);

            Role = role.ToLower();
        }
    }
}