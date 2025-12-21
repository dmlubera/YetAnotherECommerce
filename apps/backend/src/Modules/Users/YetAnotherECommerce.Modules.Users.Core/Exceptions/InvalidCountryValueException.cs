using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions;

public class InvalidCountryValueException() : YetAnotherECommerceException("Country has invalid value.")
{
    public override string ErrorCode => "invalid_country_value";
}