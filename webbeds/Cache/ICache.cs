using System; 

namespace webbeds.Cache
{
    public interface ICache
    { 
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? timeSpan = null); 
    }
}
