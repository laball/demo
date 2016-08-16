using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    public class CacheAttribute : Attribute
    {
        public string Key { get;  set; }

        //可以考虑增加一些其他缓存控制属性，例如失效时间等

        public CacheAttribute()
        {

        }

        public CacheAttribute(string Key,TimeSpan expire)
        {
            this.Key = Key;
        }
    }
}
