using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Shared.Infrastructure.Cache;

public class InMemoryCache(IMemoryCache memoryCache, IOptions<CacheSettings> options) : ICache
{
    private readonly CacheSettings _cacheSettings = options.Value;

    public T Get<T>(ICacheKey<T> key)
        => memoryCache.Get<T>(key);

    public void Set<T>(ICacheKey<T> key, T value)
    {
        var cachedObjectName = value.GetType().Name;
        var timespan = _cacheSettings.Expirations[cachedObjectName];
        memoryCache.Set(key, value, timespan);
    }

    public void Clear<T>(ICacheKey<T> key)
        => memoryCache.Remove(key);
}