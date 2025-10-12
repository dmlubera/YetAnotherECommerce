using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class EmailInUseException : YetAnotherECommerceException
    {
        public override string ErrorCode => "email_in_use";
        
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}