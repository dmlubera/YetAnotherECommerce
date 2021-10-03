using System;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Email Email { get; set; }
        public Password Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        protected User() { }

        public User(string email, string password, string role)
        {
            Id = Guid.NewGuid();
            Email = Email.Create(email);
            Password = Password.Create(password);
            SetRole(role);
            CreatedAt = DateTime.UtcNow;
        }

        private void SetRole(string role)
        {
            if (!Enum.IsDefined(typeof(AllowedRoles), role.ToLower()))
                throw new RoleNotExistException(role);

            Role = role.ToLower();
        }
    }
}