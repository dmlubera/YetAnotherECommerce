using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using YetAnotherECommerce.Functions.Builders;
using YetAnotherECommerce.Functions.Services;
using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Functions;

public class EmailNotificationFunction(IEmailMessageBuilderFactory emailMessageBuilderFactory,
    IEmailSender emailSender)
{
    [Function(nameof(EmailNotificationFunction))]
    public async Task Run(
        [ServiceBusTrigger("notification.events", "notification.dispatcher",
            Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message)
    {
        var eventType = message.ApplicationProperties["eventType"]?.ToString();
        if (string.IsNullOrWhiteSpace(eventType))
            throw new ArgumentException("Missing eventType in ServiceBus message.");

        var builder = emailMessageBuilderFactory.GetBuilder(eventType);

        var payloadJson = Encoding.UTF8.GetString(message.Body.ToArray());
        var notification = (INotification)JsonSerializer.Deserialize(payloadJson, builder.NotificationType)!;

        if (notification is null)
            throw new InvalidOperationException($"Failed to deserialize notification for eventType '{eventType}'.");

        var emailMessage = await builder.BuildEmailMessageAsync(notification);

        await emailSender.SendAsync(emailMessage);
    }
}