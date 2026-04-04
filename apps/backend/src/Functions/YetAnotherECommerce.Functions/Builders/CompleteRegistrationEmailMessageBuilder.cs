using YetAnotherECommerce.Functions.Models;
using YetAnotherECommerce.Shared.Contracts.Notifications.Identity;

namespace YetAnotherECommerce.Functions.Builders;

public class CompleteRegistrationEmailMessageBuilder : EmailMessageBuilder<UserRegistered>
{
    public override string EventType => "user.registered";
    public override Type NotificationType => typeof(UserRegistered);
    public override string TemplateName => "complete-registration";
    public override string Subject => "Welcome to YetAnotherECommerce!";

    protected override Task<EmailMessage> BuildEmailMessageAsync(UserRegistered notification, string template)
    {
        var body = template.Replace("{{completionLink}}", notification.EmailConfirmationLink)
            .Replace("{{year}}", DateTime.UtcNow.Year.ToString());

        return Task.FromResult(new EmailMessage(notification.Email, Subject, body));
    }
}