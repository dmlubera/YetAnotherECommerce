using Microsoft.Extensions.Caching.Memory;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Shared.Infrastructure.Cache;

public class InMemoryCache(IMemoryCache memoryCache) : ICache
{
    public T Get<T>(string key)
        => memoryCache.Get<T>(key);

    public void Set<T>(string key, T value)
        => memoryCache.Set(key, value);

    public void Clear(string key)
        => memoryCache.Remove(key);
}