using System;
using System.Runtime.Caching;

namespace System.Runtime.Caching
{
    public static class ObjectCacheExtensions
    {
        public static T Get<T>(ObjectCache cache, string key, T @default = default(T))
        {
            object cachedItem = cache.Get(key);
            if (cachedItem is T t)
                return t;
            return @default;
        }

        public static bool TryGetValue<T>(ObjectCache cache, string key, out T value)
        {
            object cachedItem = cache.Get(key);
            if (cachedItem is T t)
            {
                value=t;
                return true;
            }
            value = default(T);
            return false;
        }

        public static T AddOrGetExisting<T>(ObjectCache cache, string key, Func<(T item, CacheItemPolicy policy)> addFunc)
        {
            object cachedItem = cache.Get(key);
            if (cachedItem is T t)
                return t;
            (T item, CacheItemPolicy policy) = addFunc();
            cache.Add(key, item, policy);
            return item;
        }

        public static T AddOrGetExisting<T>(ObjectCache cache, string key, Func<string, (T item, CacheItemPolicy policy)> addFunc)
        {
            object cachedItem = cache.Get(key);
            if (cachedItem is T t)
                return t;
            (T item, CacheItemPolicy policy) = addFunc(key);
            cache.Add(key, item, policy);
            return item;
        }
    }
}
