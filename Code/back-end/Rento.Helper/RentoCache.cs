using System;
using System.Runtime.Caching;

namespace Rento.Helper
{
    public class RentoCache
    {
        public static void Set(string key, object value, int hour = 24)
        {
            try
            {
                MemoryCache.Default.Set(key, value,DateTime.Now.AddHours(hour));
            }
            catch (Exception e)
            {
                Logger.Exception(e, "RentoCache - Set, Key: " + key);
                throw e;
            }
        }
        public static T Get<T>(string key)
        {
            try
            {
                var value = MemoryCache.Default.Get(key);
                if (value == null)
                    return default(T);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception e)
            {
                Logger.Exception(e, "RentoCache - Get, Key: " + key);
                throw e;
            }
        }
    }
}
