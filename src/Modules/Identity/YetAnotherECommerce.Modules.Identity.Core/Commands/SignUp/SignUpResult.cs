using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;

public record SignUpResult : Result
{
    private SignUpResult()
    {
    }

    private SignUpResult(Error error) : base(error)
    {
    }
    
    public static SignUpResult Succeeded() => new();

    public static SignUpResult Failed() => new(new SignUpFailedError());
}

public record SignUpFailedError() : Error("sign_up_failed", "Sign up failed");