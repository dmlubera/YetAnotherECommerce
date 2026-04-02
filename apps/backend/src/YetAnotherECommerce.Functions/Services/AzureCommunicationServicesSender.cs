using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using YetAnotherECommerce.Functions.Settings;
using EmailMessage = YetAnotherECommerce.Functions.Models.EmailMessage;

namespace YetAnotherECommerce.Functions.Services;

public class AzureCommunicationServicesSender(EmailClient emailClient, IOptions<EmailNotificationsSettings> options) : IEmailSender
{
    private readonly EmailNotificationsSettings  _settings = options.Value;

    public async Task SendAsync(EmailMessage emailMessage)
    {
        var message = new Azure.Communication.Email.EmailMessage(
            senderAddress: _settings.NoReplyEmailAddress,
            recipientAddress: emailMessage.To,
            new EmailContent(emailMessage.Subject)
            {
                Html = emailMessage.Body
            });
        
        await emailClient.SendAsync(WaitUntil.Completed, message);
    }
}