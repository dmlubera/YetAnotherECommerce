using EmailMessage = YetAnotherECommerce.Functions.Models.EmailMessage;

namespace YetAnotherECommerce.Functions.Services;

public interface IEmailSender
{
    Task SendAsync(EmailMessage emailMessage);
}