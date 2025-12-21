using System;

namespace YetAnotherECommerce.Shared.Abstractions.Exceptions;

public interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}