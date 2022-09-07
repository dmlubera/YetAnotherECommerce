using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Infrastructure.Correlation
{
    public class CorrelationMiddleware : IMiddleware
    {
        private readonly ICorrelationContext _correlationId;

        public CorrelationMiddleware(ICorrelationContext correlationContext)
            => _correlationId = correlationContext;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _correlationId.CorrelationId = Guid.NewGuid().ToString();
            using (LogContext.PushProperty(_correlationId.CorrelationIdKey, _correlationId.CorrelationId))
            {
                await next(context);
            }
        }
    }
}