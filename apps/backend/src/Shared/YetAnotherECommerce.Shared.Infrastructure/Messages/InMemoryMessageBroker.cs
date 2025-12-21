using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public class InMemoryMessageBroker(
    IMessageClient messageClient,
    IAsyncMessageDispatcher asyncMessageDispatcher,
    ICorrelationContext correlationContext,
    IOptions<MessagingOptions> messagingOptions)
    : IMessageBroker
{
    private readonly MessagingOptions _messagingOptions = messagingOptions.Value;

    public async Task PublishAsync(IMessage message)
    {
        if (message is null)
            return;

        var metadata = new Dictionary<string, string>
        {
            [correlationContext.CorrelationIdKey] = correlationContext.CorrelationId
        };
        var messageEnvelope = new MessageEnvelope(metadata, message);

        if (_messagingOptions.UseAsyncDispatcher)
        {
            await asyncMessageDispatcher.PublishAsync(messageEnvelope);
        }
        else
        {
            await messageClient.PublishAsync(messageEnvelope);
        }
    }
}