using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class InvalidPasswordSaltException() : YetAnotherECommerceException("Salt cannot be null or whitespace.")
{
    public override string ErrorCode => "invalid_password_salt";
}