using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail
{
    public class ChangeEmailCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        private ChangeEmailCommand() { }

        public ChangeEmailCommand(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}