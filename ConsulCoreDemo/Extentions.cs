using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace ConsulCoreDemo
{
    public static class Extentions
    {
        public static byte[] GetBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static T Sync<T>(this Task<T> task)
        {
            return AsyncContext.Run(() => task);
        }

        public static void Sync(this Task task)
        {
            AsyncContext.Run(() => task);
        }

        public static List<T> SortEx<T>(this List<T> list)
        {
            list?.Sort();
            return list;
        }
    }
}