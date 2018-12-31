using System.Threading.Tasks;
using Orleans.Concurrency;

namespace Orleans.Interfaces
{
    public interface IDemoService : IGrainWithIntegerKey
    {
        /// <summary>
        /// 使用Immutable优化复制
        /// see http://www.cnblogs.com/liwt/p/orleans-immutable.html
        /// </summary>
        /// <returns></returns>
        Task<Immutable<string>> SayHello();
    }
}
