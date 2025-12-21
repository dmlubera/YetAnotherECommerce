using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public class BackroundMessageDispatcher(
    IMessageChannel messageChannel,
    IMessageClient messageClient,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<BackroundMessageDispatcher> logger,
    ICorrelationContext correlationContext)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var message in messageChannel.Reader.ReadAllAsync())
        {
            using var scope = serviceScopeFactory.CreateScope();
            correlationContext.CorrelationId = message.Metadata.SingleOrDefault().Value;
            using (LogContext.PushProperty(correlationContext.CorrelationIdKey, correlationContext.CorrelationId))
            {
                try
                {
                    await messageClient.PublishAsync(message);
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }
        }
    }
}