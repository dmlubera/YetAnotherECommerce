namespace YetAnotherECommerce.Shared.Abstractions.Cache;

public interface ICacheKey<T>
{
    string CacheKey { get; }
}