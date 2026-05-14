using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.RequestPasswordReset;

public record RequestPasswordResetResult : Result
{
    public static RequestPasswordResetResult Succeeded() => new();
}