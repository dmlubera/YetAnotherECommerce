using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    internal sealed class MessageClient : IMessageClient
    {
        private readonly IMessageRegistry _messageRegistry;

        public MessageClient(IMessageRegistry messageRegistry)
        {
            _messageRegistry = messageRegistry;
        }

        public async Task PublishAsync(IMessage message)
        {
            var registrations = _messageRegistry
                .GetRegistrations(message.GetType().Name)
                .Where(x => x.Type != message.GetType());

            var tasks = new List<Task>();

            foreach(var registration in registrations)
            {
                var translatedMessage = Translate(message, registration.Type);
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