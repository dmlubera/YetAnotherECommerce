using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class InvalidEmailFormatException() : YetAnotherECommerceException("Email has invalid format.")
{
    public override string ErrorCode => "invalid_email_format";
}