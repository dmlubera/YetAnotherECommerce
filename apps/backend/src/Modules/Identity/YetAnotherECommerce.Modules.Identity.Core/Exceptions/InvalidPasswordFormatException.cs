using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class InvalidPasswordFormatException() : YetAnotherECommerceException("Given password has invalid format.")
{
    public override string ErrorCode => "invalid_password_format";
}