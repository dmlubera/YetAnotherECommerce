using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class ProvidedPasswordIsExactlyTheSameAsTheCurrentOne : YetAnotherECommerceException
    {
        public override string ErrorCode => "password_is_exactly_the_same_as_current_one";

        public ProvidedPasswordIsExactlyTheSameAsTheCurrentOne()
            : base("Password is exactly the same as current one.")
        {

        }
    }
}