using System;

namespace Summary.Framework.Task
{
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

        public void OnCompleted(Action continuation)
        {

        }

        public void UnsafeOnCompleted(Action continuation)
        {
        }
    }
}