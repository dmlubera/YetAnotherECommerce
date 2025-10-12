using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;

public record ChangePasswordResult : Result
{
    private ChangePasswordResult()
    {
    }

    private ChangePasswordResult(Error error) : base(error)
    {
    }
    
    public static ChangePasswordResult Succeeded() => new();
    
    public static ChangePasswordResult Failed() => new(new ChangePasswordFailedError());
}

public record ChangePasswordFailedError() : Error("change_password_failed", "Changing password failed.");