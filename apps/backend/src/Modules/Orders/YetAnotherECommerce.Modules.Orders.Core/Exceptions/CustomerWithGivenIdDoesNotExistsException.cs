using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions
{
    public class CustomerWithGivenIdDoesNotExistsException : YetAnotherECommerceException
    {
        public override string ErrorCode => "customer_not_exists";

        public CustomerWithGivenIdDoesNotExistsException(Guid id)
            : base($"Customer with ID: {id} does not exists.")
        {

        }
    }
}