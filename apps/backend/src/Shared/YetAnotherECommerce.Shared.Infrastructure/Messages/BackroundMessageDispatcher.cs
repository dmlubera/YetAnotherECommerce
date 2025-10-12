using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class BackroundMessageDispatcher : BackgroundService
    {
        private readonly IMessageChannel _messageChannel;
        private readonly IMessageClient _messageClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICorrelationContext _correlationContext;
        private readonly ILogger<BackroundMessageDispatcher> _logger;

        public BackroundMessageDispatcher(IMessageChannel messageChannel,
            IMessageClient messageClient,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<BackroundMessageDispatcher> logger,
            ICorrelationContext correlationContext)
        {
            _messageChannel = messageChannel;
            _messageClient = messageClient;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var message in _messageChannel.Reader.ReadAllAsync())
            {
                using var scope = _serviceScopeFactory.CreateScope();
                _correlationContext.CorrelationId = message.Metadata.SingleOrDefault().Value;
                using (LogContext.PushProperty(_correlationContext.CorrelationIdKey, _correlationContext.CorrelationId))
                {
                    try
                    {
                        await _messageClient.PublishAsync(message);
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }
            }
        }
    }
}