namespace YetAnotherECommerce.Shared.Infrastructure.Correlation
{
    public interface ICorrelationContext
    {
        string CorrelationId { get; set; }
        string CorrelationIdKey { get; }
    }
}