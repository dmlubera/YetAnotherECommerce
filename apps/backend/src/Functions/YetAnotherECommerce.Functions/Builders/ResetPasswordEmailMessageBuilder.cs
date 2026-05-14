using YetAnotherECommerce.Functions.Models;
using YetAnotherECommerce.Shared.Contracts.Notifications.Identity;

namespace YetAnotherECommerce.Functions.Builders;

public class ResetPasswordEmailMessageBuilder : EmailMessageBuilder<PasswordResetRequested>
{
    public override string EventType => "password.reset.requested";
    public override Type NotificationType => typeof(PasswordResetRequested);
    public override string TemplateName => "reset-password";
    public override string Subject => "Reset Your YetAnotherECommerce Password";
    protected override Task<EmailMessage> BuildEmailMessageAsync(PasswordResetRequested notification, string template)
    {
        var body = template.Replace("{{resetLink}}", notification.PasswordResetLink)
            .Replace("{{year}}", DateTime.UtcNow.Year.ToString());

        return Task.FromResult(new EmailMessage(notification.Email, Subject, body));
    }
}