using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword
{
    public class ChangePasswordCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }

        private ChangePasswordCommand() { }

        public ChangePasswordCommand(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}