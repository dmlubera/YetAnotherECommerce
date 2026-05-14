using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ResetPassword;

public record ResetPasswordResult : Result
{
    private ResetPasswordResult()
    {
    }

    private ResetPasswordResult(Error error) : base(error)
    {
    }

    public static ResetPasswordResult Succeeded() => new();
    public static ResetPasswordResult Failed() => new(new ResetPasswordFailed());
    public static ResetPasswordResult UserNotFound() => new(new UserNotFoundError());
}

public record UserNotFoundError() : Error("user_not_found", "User not found");

public record ResetPasswordFailed() : Error("reset_password_failed", "Resetting password failed");