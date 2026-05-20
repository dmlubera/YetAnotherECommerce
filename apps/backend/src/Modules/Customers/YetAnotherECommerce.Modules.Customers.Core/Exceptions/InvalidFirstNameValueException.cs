using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Customers.Core.Exceptions;

public class InvalidFirstNameValueException() : YetAnotherECommerceException("Firstname has invalid value.")
{
    public override string ErrorCode => "invalid_firstname";
}