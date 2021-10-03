using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail
{
    public class ChangeEmailCommand : ICommand
    {
        public string Email { get; set; }

        private ChangeEmailCommand() { }

        public ChangeEmailCommand(string email)
            => Email = email;
    }
}
