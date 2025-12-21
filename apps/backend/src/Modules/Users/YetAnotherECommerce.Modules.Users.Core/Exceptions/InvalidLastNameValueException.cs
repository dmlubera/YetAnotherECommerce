using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions;

public class InvalidLastNameValueException() : YetAnotherECommerceException("Lastname has invalid value.")
{
    public override string ErrorCode => "invalid_lastname";
}