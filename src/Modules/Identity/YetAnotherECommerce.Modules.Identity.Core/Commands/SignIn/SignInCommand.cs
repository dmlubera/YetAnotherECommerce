using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn
{
    public class SignInCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CacheKey { get; set; }

        private SignInCommand() { }

        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
            CacheKey = $"jwt-{Guid.NewGuid()}";
        }
    }
}