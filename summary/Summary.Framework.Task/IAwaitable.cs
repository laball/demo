using System.Runtime.CompilerServices;

namespace Summary.Framework.Task
{
    /// <summary>
    /// 支持 await 的类型 GetAwaiter 需要返回的对象必须实现的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAwaitable<T> : ICriticalNotifyCompletion
    {
        T GetResult();
        bool IsCompleted { get; }
    }
}