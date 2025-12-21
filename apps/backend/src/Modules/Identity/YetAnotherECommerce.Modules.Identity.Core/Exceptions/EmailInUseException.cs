using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class EmailInUseException() : YetAnotherECommerceException("Email is already in use.")
{
    public override string ErrorCode => "email_in_use";
}