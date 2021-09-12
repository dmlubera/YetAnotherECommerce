using System;

namespace YetAnotherECommerce.Modules.Users.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}