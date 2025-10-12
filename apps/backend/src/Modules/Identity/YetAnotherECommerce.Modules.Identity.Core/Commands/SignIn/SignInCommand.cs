using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

public record SignInCommand(string Email, string Password) : ICommand<SignInResult>;