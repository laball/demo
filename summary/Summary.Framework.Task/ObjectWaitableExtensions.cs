namespace Summary.Framework.Task
{
    /// <summary>
    /// 为任意类型扩展支持 GetAwaiter 方法；
    /// 即任何类型可以直接使用 await 
    /// </summary>
    public static class ObjectWaitableExtensions
    {
        public static ObjectWaitable<T> GetAwaiter<T>(this T value) => new ObjectWaitable<T>(value);
    }
}