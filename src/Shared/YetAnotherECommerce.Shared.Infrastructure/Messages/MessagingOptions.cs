namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class MessagingOptions
    {
        public bool UseAsyncDispatcher { get; set; }
        public bool UseAzureServiceBus { get; set; }
    }
}