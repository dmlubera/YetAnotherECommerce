using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Shared.Infrastructure.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorCode = "internal_server_error";
            var errorMessage = "Oops, something went wrong. Please try again later.";

            if(ex is YetAnotherECommerceException e)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorCode = e.ErrorCode;
                errorMessage = e.Message;
            }

            var payload = new { ErrorCode = errorCode, ErrorMessage = errorMessage };
            httpContext.Response.StatusCode = (int)statusCode;

            await httpContext.Response.WriteAsJsonAsync(payload);
        }
    }
}