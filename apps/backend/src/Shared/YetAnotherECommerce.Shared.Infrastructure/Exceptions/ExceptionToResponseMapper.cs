using System;
using System.Net;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Shared.Infrastructure.Exceptions
{
    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
         => exception switch
         {
             YetAnotherECommerceException ex => new ExceptionResponse(new ErrorsResponse(new Error(ex.ErrorCode, ex.Message)), HttpStatusCode.BadRequest),
             _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "Oops, someting went wrong.")), HttpStatusCode.InternalServerError)
         };

        private record Error(string Code, string Message);
        private record ErrorsResponse(params Error[] Errors);
    }
}