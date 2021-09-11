using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp
{
    public class SignUpCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public SignUpCommand(string email, string password)
            => (Email, Password) = (email, password);
    }
}