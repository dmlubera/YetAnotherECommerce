using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions;

public class InvalidZipCodeValueException() : YetAnotherECommerceException("Zip code has invalid value.")
{
    public override string ErrorCode => "invalid_zipcode_value";
}