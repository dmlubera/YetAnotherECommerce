using System;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Email Email { get; set; }
        public Password Password { get; set; }

        protected User() { }

        public User(string email, string passwordHash, string passwordSalt)
        {
            Id = Guid.NewGuid();
            Email = Email.Create(email);
            Password = Password.Create(passwordHash, passwordSalt);
        }
    }
}