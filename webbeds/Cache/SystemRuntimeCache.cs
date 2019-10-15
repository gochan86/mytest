using System;
using System.Configuration;
using System.Runtime.Caching;

namespace webbeds.Cache
{
    public class SystemRuntimeCache : ICache
    {
        private readonly ObjectCache _cache = MemoryCache.Default;
        private double defaultExpirationTime = 20; 

        internal SystemRuntimeCache()
        {
            if (ConfigurationManager.AppSettings["CacheExpirationTime"] != null)
                defaultExpirationTime = Double.Parse(ConfigurationManager.AppSettings["CacheExpirationTime"]);
        } 
        public T Get<T>(string key)
        {
            return (T)_cache[key];
        }

        public void Set<T>(string key, T value, TimeSpan? timeSpan = null)
        {

            if (timeSpan == null)
                timeSpan = TimeSpan.FromMinutes(defaultExpirationTime);

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.Add(timeSpan.Value) };
            if (value != null)
            {
                _cache.Remove(key);
                _cache.Set(key, value, policy);
            }
        }    
    }
}