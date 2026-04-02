namespace YetAnotherECommerce.Functions.Builders;

public interface IEmailMessageBuilderFactory
{
    IEmailMessageBuilder GetBuilder(string eventType);
}