using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp
{
    public class SignUpCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        private SignUpCommand() { }

        public SignUpCommand(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
    }
}