using Microsoft.Extensions.Caching.Memory;

namespace Checkin.Repositories
{
    public class CacheRepository<T> : ICacheRepository<T>
    {
        private readonly IMemoryCache memoryCache;

        public CacheRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public T Get(string cacheKey)
        {
            if(memoryCache.TryGetValue(cacheKey, out T cacheItems))
            {
                return cacheItems;
            }

            return (T)default;
        }

        public void Set(string cacheKey, T value)
        {
            memoryCache.Set(cacheKey, value);
        }
    }
}