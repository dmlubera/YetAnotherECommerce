using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class RoleNotExistException(string role)
    : YetAnotherECommerceException($"Specified role: {role} does not exist.")
{
    public override string ErrorCode => "role_not_exist";
}