using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.RequestPasswordReset;

public record RequestPasswordResetCommand(string Email) : ICommand<RequestPasswordResetResult>;