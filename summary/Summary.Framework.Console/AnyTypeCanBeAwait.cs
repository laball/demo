using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Summary.Framework.Console
{
    public class AnyTypeCanBeAwait
    {
        /// <summary>
        /// 可以 await 的类型，需要满足的条件：
        /// 1- 支持 GetAwaiter 方法；一般使用扩展方法即可；
        /// 2- GetAwaiter 返回的对象支持 
        ///    2-1 GetResult()
        ///    2-2 bool IsCompleted { get; }
        ///    2-3 实现 INotifyCompletion 接口
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static async Task<T> Test<T>(T n) => await n;
    }

    /// <summary>
    /// 支持 await 的类型 GetAwaiter 需要返回的对象必须实现的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAwaitable<T> : INotifyCompletion
    {
        T GetResult();
        bool IsCompleted { get; }
    }

    /// <summary>
    /// 任意类型 GetAwaiter 方法返回类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectWaitable<T> : IAwaitable<T>
    {
        private readonly T m_value;

        public ObjectWaitable(T value) => m_value = value;

        public bool IsCompleted => true;

        public T GetResult() => m_value;

        public void OnCompleted(Action continuation) => continuation?.Invoke();
    }

    /// <summary>
    /// 为任意类型扩展支持 GetAwaiter 方法；
    /// </summary>
    public static class ObjectWaitableExtensions
    {
        public static ObjectWaitable<T> GetAwaiter<T>(this T value) => new ObjectWaitable<T>(value);
    }
}