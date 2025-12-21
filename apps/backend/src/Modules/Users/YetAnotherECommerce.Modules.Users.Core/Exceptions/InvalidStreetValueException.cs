using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions;

public class InvalidStreetValueException() : YetAnotherECommerceException("Street has invalid value.")
{
    public override string ErrorCode => "invalid_street_value";
}