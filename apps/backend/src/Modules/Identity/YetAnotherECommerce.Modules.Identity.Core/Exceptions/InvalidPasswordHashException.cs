using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class InvalidPasswordHashException() : YetAnotherECommerceException("Hash cannot be null or whitespace.")
{
    public override string ErrorCode => "invalid_password_hash";
}