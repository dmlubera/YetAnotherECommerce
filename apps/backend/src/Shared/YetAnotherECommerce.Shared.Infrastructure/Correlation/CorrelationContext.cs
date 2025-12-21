using System.Threading;

namespace YetAnotherECommerce.Shared.Infrastructure.Correlation;

internal class CorrelationContext : ICorrelationContext
{
    private static readonly AsyncLocal<string> _correlationId = new();
    public string CorrelationId { get { return _correlationId.Value; } set { _correlationId.Value = value; } } 
    public string CorrelationIdKey { get; } = "CorrelationId";
}