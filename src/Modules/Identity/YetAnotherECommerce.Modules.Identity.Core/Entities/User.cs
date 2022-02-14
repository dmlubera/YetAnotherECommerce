﻿using System;
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

        private User(Email email, Password password, string role)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            SetRole(role);
        }

        public static User Create(Email email, Password password, string role)
            => new User(email, password, role);

        public void ChangeEmail(string email)
        {
            Email = Email.Create(email);

            AddEvent(new EmailChanged(this, Email));
        }

        public void ChangePassword(Password password)
        {
            Password = password;

            AddEvent(new PasswordChanged(this, Password));
        }

        private void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role) || !Enum.IsDefined(typeof(AllowedRoles), role.ToLower()))
                throw new RoleNotExistException(role);

            Role = role.ToLower();
        }
    }
}