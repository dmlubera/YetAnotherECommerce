using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class ProvidedEmailIsExactlyTheSameAsTheCurrentOneException : YetAnotherECommerceException
    {
        public override string ErrorCode => "provided_email_is_exactly_the_same_as_the_current_one";

        public ProvidedEmailIsExactlyTheSameAsTheCurrentOneException()
            : base("Cannot change email due to provided email is exactly the same as the current one.")
        {

        }
    }
}