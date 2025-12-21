using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Shared.Infrastructure.Exceptions;

internal class ExceptionHandlerMiddleware(
    ILogger<ExceptionHandlerMiddleware> logger,
    IExceptionToResponseMapper exceptionToResponseMapper)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var errorResponse = exceptionToResponseMapper.Map(ex);
        context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = errorResponse?.Response;

        if (response is null)
            return;

        await context.Response.WriteAsJsonAsync(response);
    }
}