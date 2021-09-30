using Microsoft.Extensions.Caching.Memory;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Shared.Infrastructure.Cache
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
            => _memoryCache.Get<T>(key);

        public void Set<T>(string key, T value)
            => _memoryCache.Set(key, value);

        public void Clear(string key)
            => _memoryCache.Remove(key);
    }
}