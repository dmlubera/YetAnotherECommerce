using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;

public record SignUpCommand(string Email, string Password, string Role) : ICommand<SignUpResult>;