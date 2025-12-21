using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Infrastructure.Correlation;

public class CorrelationMiddleware(ICorrelationContext correlationContext) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        correlationContext.CorrelationId = Guid.NewGuid().ToString();
        using (LogContext.PushProperty(correlationContext.CorrelationIdKey, correlationContext.CorrelationId))
        {
            await next(context);
        }
    }
}