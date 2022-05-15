using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IMessageClient _messageClient;
        private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
        private readonly MessagingOptions _messagingOptions;

        public InMemoryMessageBroker(IMessageClient messageClient,
            IAsyncMessageDispatcher asyncMessageDispatcher, IOptions<MessagingOptions> messagingOptions)
        {
            _messageClient = messageClient;
            _asyncMessageDispatcher = asyncMessageDispatcher;
            _messagingOptions = messagingOptions.Value;
        }

        public async Task PublishAsync(IMessage message)
        {
            if (message is null)
                return;

            if (_messagingOptions.UseAsyncDispatcher)
            {
                await _asyncMessageDispatcher.PublishAsync(message);
            }
            else
            {
                await _messageClient.PublishAsync(message);
            }
        }
    }
}