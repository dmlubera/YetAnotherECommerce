using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using YetAnotherECommerce.Functions.Models;
using YetAnotherECommerce.Functions.Settings;

namespace YetAnotherECommerce.Functions.Services;

public interface IEmailSender
{
    Task SendAsync(EmailMessage emailMessage);
}

public class SmtpEmailSender(IOptions<EmailNotificationsSettings> options) : IEmailSender
{
    private readonly EmailNotificationsSettings  _settings = options.Value;

    public async Task SendAsync(EmailMessage emailMessage)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.NoReplyAddressEmail));
        message.To.Add(MailboxAddress.Parse(emailMessage.To));
        message.Subject = emailMessage.Subject;
        message.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("localhost", 1025);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}