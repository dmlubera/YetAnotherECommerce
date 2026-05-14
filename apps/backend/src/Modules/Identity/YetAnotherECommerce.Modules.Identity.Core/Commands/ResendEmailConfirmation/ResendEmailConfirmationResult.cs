using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ResendEmailConfirmation;

public record ResendEmailConfirmationResult : Result
{
    private ResendEmailConfirmationResult()
    {
    }

    public static ResendEmailConfirmationResult Succeeded() => new();
}