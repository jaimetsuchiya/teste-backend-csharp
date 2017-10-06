using System;
using System.Runtime.Caching;

namespace Infrastructure.TorreHanoi.Cache
{
    public class CacheManager : ICacheManager 
    {
        public object DataSource { get; set; }
        public void Set(string key)
        {
            if (DataSource != null)
            {
                var cachePolicy = new CacheItemPolicy {AbsoluteExpiration = DateTime.Now.AddMinutes(1440)};

                MemoryCache.Default.Add(
                    new CacheItem(key, DataSource), cachePolicy);
            }
            else
            {
                throw new ArgumentNullException("DataSource");
            }
        }

        public object Get(string key)
        {
            return MemoryCache.Default.GetCacheItem(key) != null ? MemoryCache.Default.GetCacheItem(key)?.Value : null;
        }
    }
}
