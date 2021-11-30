using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IMessageClient _messageClient;

        public InMemoryMessageBroker(IMessageClient messageClient)
        {
            _messageClient = messageClient;
        }

        public async Task PublishAsync(IMessage message)
        {
            if (message is null)
                return;

            await _messageClient.PublishAsync(message);
        }
    }
}