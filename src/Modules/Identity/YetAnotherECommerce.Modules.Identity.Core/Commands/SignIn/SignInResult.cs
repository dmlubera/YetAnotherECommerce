using YetAnotherECommerce.Modules.Identity.Core.Dtos;
using YetAnotherECommerce.Shared.Abstractions.Results;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

public record SignInResult : Result<JsonWebToken>
{
    private SignInResult(JsonWebToken token) : base(token)
    {
    }

    private SignInResult(Error error) : base(error)
    {
    }

    public static SignInResult Succeeded(JsonWebToken token) => new(token);

    public static SignInResult InvalidCredentials() => new(new InvalidCredentialsError());
}

public record InvalidCredentialsError() : Error("invalid_credentials", "Invalid credentials");