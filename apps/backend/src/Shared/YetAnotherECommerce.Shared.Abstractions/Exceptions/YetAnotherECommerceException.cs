using System;

namespace YetAnotherECommerce.Shared.Abstractions.Exceptions;

public abstract class YetAnotherECommerceException(string message) : Exception(message)
{
    public abstract string ErrorCode { get; }
}