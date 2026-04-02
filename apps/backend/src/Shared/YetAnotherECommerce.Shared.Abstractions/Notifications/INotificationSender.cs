using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Notifications;

public interface INotificationSender
{
    Task SendAsync(INotification notification);
}