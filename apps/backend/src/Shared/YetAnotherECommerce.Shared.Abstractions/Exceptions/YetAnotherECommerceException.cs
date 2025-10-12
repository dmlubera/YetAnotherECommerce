using System;

namespace YetAnotherECommerce.Shared.Abstractions.Exceptions
{
    public abstract class YetAnotherECommerceException : Exception
    {
        public abstract string ErrorCode { get; }

        protected YetAnotherECommerceException(string message) : base(message)
        {

        }
    }
}