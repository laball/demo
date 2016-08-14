using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PostSharpDemo
{
    public class TestCacheImp : ICache
    {
        private IDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        public object this[string key]
        {
            get
            {
                if (cache.ContainsKey(key))
                {
                    return cache[key];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                cache[key] = value;
            }
        }
    }
}