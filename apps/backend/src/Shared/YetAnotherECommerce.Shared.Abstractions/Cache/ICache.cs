namespace YetAnotherECommerce.Shared.Abstractions.Cache;

public interface ICache
{
    T Get<T>(string key);
    void Set<T>(string key, T value);
    void Clear(string key);
}