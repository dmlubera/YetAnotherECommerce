using System.Reflection;
using System.Text;
using YetAnotherECommerce.Functions.Models;
using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Functions.Builders;

public abstract class EmailMessageBuilder<TNotification> : IEmailMessageBuilder where TNotification : INotification
{
    public abstract string EventType { get; }
    public abstract Type NotificationType { get; }
    public abstract string TemplateName { get; }
    public abstract string Subject { get; }

    public async Task<EmailMessage> BuildEmailMessageAsync(INotification notification)
        => await BuildEmailMessageAsync((TNotification)notification, GetTemplate());

    protected abstract Task<EmailMessage> BuildEmailMessageAsync(TNotification notification, string template);

    private string GetTemplate()
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{assemblyName}.Templates.{TemplateName}.html")!;
        using var streamReader  = new StreamReader(stream, Encoding.UTF8);
        return streamReader.ReadToEnd();
    }
}