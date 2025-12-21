using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class InvalidCredentialsException() : YetAnotherECommerceException("Given credentials are invalid.")
{
    public override string ErrorCode => "invalid_credentials";
}