using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ConfirmEmail;

public record ConfirmEmailResult : Result
{
    private ConfirmEmailResult()
    {
    }

    private ConfirmEmailResult(Error error) : base(error)
    {
    }

    public static ConfirmEmailResult Succeeded() => new();

    public static ConfirmEmailResult UserNotFound() => new(new UserNotFoundError());

    public static ConfirmEmailResult InvalidToken() => new(new InvalidTokenError());
}

public record UserNotFoundError() : Error("user_not_found", "User not found");

public record InvalidTokenError() : Error("invalid_token", "Invalid token");