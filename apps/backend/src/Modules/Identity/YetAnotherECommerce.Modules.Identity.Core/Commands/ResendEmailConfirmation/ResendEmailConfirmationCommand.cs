using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ResendEmailConfirmation;

public record ResendEmailConfirmationCommand(string Email) : ICommand<ResendEmailConfirmationResult>;