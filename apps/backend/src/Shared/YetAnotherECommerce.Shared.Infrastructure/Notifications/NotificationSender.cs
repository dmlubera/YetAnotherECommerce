using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Shared.Infrastructure.Notifications;

public class NotificationSender(IMessagePublisher messagePublisher) : INotificationSender
{
    public async Task SendAsync(INotification notification)
    {
        await messagePublisher.PublishAsync(notification, "notification");
    }
}