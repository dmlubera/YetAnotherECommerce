using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions;

public class InvalidCityValueException() : YetAnotherECommerceException("City has invalid value.")
{
    public override string ErrorCode => "invalid_city_value";
}