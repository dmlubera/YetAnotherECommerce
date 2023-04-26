using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IMessageClient _messageClient;
        private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
        private readonly ICorrelationContext _correlationContext;
        private readonly MessagingOptions _messagingOptions;
        private readonly IEnumerable<ServiceBusSender> _serviceBusSender;

        public InMemoryMessageBroker(IMessageClient messageClient,
            IAsyncMessageDispatcher asyncMessageDispatcher,
            ICorrelationContext correlationContext,
            IOptions<MessagingOptions> messagingOptions,
            IEnumerable<ServiceBusSender> serviceBusSender)
        {
            _messageClient = messageClient;
            _asyncMessageDispatcher = asyncMessageDispatcher;
            _correlationContext = correlationContext;
            _messagingOptions = messagingOptions.Value;
            _serviceBusSender = serviceBusSender;
        }

        public async Task PublishAsync(IMessage message)
        {
            if (message is null)
                return;

            var metadata = new Dictionary<string, string>
            {
                [_correlationContext.CorrelationIdKey] = _correlationContext.CorrelationId
            };
            var messageEnvelope = new MessageEnvelope(metadata, message);

            //TODO: Refactor with config to choose message broker mode
            if (_messagingOptions.UseAzureServiceBus && _serviceBusSender.FirstOrDefault(x => x.EntityPath.Equals(message.GetType().Name, StringComparison.InvariantCultureIgnoreCase)) is { } sender)
            {
                await sender.SendMessageAsync(new ServiceBusMessage
                {
                    Body = new BinaryData(SerializeMessageAsBytes(messageEnvelope)),
                    CorrelationId = _correlationContext.CorrelationId,
                    Subject = sender.EntityPath
                });
            }
            else if (_messagingOptions.UseAsyncDispatcher)
            {
                await _asyncMessageDispatcher.PublishAsync(messageEnvelope);
            }
            else
            {
                await _messageClient.PublishAsync(messageEnvelope);
            }
        }

        private static byte[] SerializeMessageAsBytes(MessageEnvelope message)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, serializerSettings));
        }
    }
}