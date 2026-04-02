using YetAnotherECommerce.Functions.Models;
using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Functions.Builders;

public interface IEmailMessageBuilder
{
    string EventType { get; }
    Type NotificationType { get; }
    string TemplateName { get; }
    string Subject { get; }
    
    Task<EmailMessage> BuildEmailMessageAsync(INotification notification);
}