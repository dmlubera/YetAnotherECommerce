namespace YetAnotherECommerce.Shared.Abstractions.Cache;

public interface ICache
{
    T Get<T>(ICacheKey<T> key);
    void Set<T>(ICacheKey<T> key, T value);
    void Clear<T>(ICacheKey<T> key);
}