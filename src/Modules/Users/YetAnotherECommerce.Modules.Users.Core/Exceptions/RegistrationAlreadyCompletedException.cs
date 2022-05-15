using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.Exceptions
{
    public class RegistrationAlreadyCompletedException : YetAnotherECommerceException
    {
        public override string ErrorCode => "registration_already_completed";

        public RegistrationAlreadyCompletedException()
            : base("Registration has been already completed.")
        {

        }
    }
}