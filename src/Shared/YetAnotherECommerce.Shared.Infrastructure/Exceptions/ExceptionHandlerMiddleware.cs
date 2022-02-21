using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Shared.Infrastructure.Exceptions
{
    internal class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IExceptionToResponseMapper exceptionToResponseMapper)
        {
            _logger = logger;
            _exceptionToResponseMapper = exceptionToResponseMapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResponse = _exceptionToResponseMapper.Map(ex);
            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
            var response = errorResponse?.Response;

            if (response is null)
                return;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}