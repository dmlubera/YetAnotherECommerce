using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

internal sealed class MessageClient(IMessageRegistry messageRegistry, IServiceScopeFactory serviceScopeFactory)
    : IMessageClient
{
    public async Task PublishAsync(IMessageEnvelope messageEnvelope)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var registrations = messageRegistry
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

    private static object Translate(IMessage message, Type type)
        => Deserialize(Serialize(message), type);

    private static byte[] Serialize(object value)
        => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));

    private static object Deserialize(byte[] value, Type type)
        => JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
}