using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Humanizer;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public class ServiceBusMessagePublisher(ServiceBusClient serviceBusClient) : IMessagePublisher
{
    public Task PublishAsync(IMessage message)
    {
        throw new System.NotImplementedException();

    }

    public async Task PublishAsync(IMessage message, string source)
    {
        var sender = serviceBusClient.CreateSender($"{source}.events");
        var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(message, message.GetType()))
            {
                ApplicationProperties =
                {
                    ["eventType"] = GetEventType(message)
                }
            };
        await sender.SendMessageAsync(serviceBusMessage);
    }

    private static string GetEventType(IMessage message)
    {
        var parts = message.GetType().Name.Humanize(LetterCasing.LowerCase).Split(' ');
        return string.Join(".", parts);
    }
}