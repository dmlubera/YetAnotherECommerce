namespace YetAnotherECommerce.Functions.Builders;

public class EmailMessageBuilderFactory : IEmailMessageBuilderFactory
{
    private readonly IReadOnlyDictionary<string, IEmailMessageBuilder> _builders;

    public EmailMessageBuilderFactory(IEnumerable<IEmailMessageBuilder> builders)
    {
        _builders = builders.ToDictionary(x => x.EventType, x => x);
    }

    public IEmailMessageBuilder GetBuilder(string eventType)
        => _builders.TryGetValue(eventType, out var builder)
            ? builder
            : throw new ArgumentException($"No builder registered for event type '{eventType}'");
}