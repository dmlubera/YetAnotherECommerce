using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    internal sealed class MessageClient : IMessageClient
    {
        private readonly IMessageRegistry _messageRegistry;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageClient(IMessageRegistry messageRegistry, IServiceScopeFactory serviceScopeFactory)
        {
            _messageRegistry = messageRegistry;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task PublishAsync(IMessageEnvelope messageEnvelope)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var registrations = _messageRegistry
                .GetRegistrations(messageEnvelope.Payload.GetType().Name)
                .Where(x => x.Type != messageEnvelope.Payload.GetType());

            var tasks = new List<Task>();

            foreach(var registration in registrations)
            {
                var translatedMessage = Translate(messageEnvelope.Payload, registration.Type);
                tasks.Add(registration.Action((IMessage)translatedMessage));
            }

            await Task.WhenAll(tasks);
        }

        private object Translate(IMessage message, Type type)
            => Deserialize(Serialize(message), type);

        private byte[] Serialize(object value)
            => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));

        private object Deserialize(byte[] value, Type type)
            => JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}