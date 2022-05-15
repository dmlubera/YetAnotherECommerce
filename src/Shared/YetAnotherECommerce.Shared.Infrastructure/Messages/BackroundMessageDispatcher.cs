using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class BackroundMessageDispatcher : BackgroundService
    {
        private readonly IMessageChannel _messageChannel;
        private readonly IMessageClient _messageClient;

        public BackroundMessageDispatcher(IMessageChannel messageChannel, IMessageClient messageClient)
        {
            _messageChannel = messageChannel;
            _messageClient = messageClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var message in _messageChannel.Reader.ReadAllAsync())
            {
                try
                {
                    await _messageClient.PublishAsync(message);
                }
                catch (Exception)
                {
                    //TODO: Add logging
                }
            }
        }
    }
}