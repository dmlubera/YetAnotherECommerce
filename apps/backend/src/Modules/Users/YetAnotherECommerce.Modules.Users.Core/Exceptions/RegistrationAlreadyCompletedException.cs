using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions;

public class RegistrationAlreadyCompletedException()
    : YetAnotherECommerceException("Registration has been already completed.")
{
    public override string ErrorCode => "registration_already_completed";
}