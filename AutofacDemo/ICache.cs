using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    public interface ICache
    {
        T Get<T>(string key);
    }

    public class FackCacheImp : ICache
    {
        public T Get<T>(string key)
        {
            return default(T);
        }
    }

}
