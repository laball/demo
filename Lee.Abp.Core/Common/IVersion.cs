using System;

namespace Lee.Abp.Core.Common
{
    /// <summary>
    /// 定义泛型接口，提升抽象性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVersion<T>
    {
        T Version { get; set; }
    }

    /// <summary>
    /// 默认使用时间戳作为版本
    /// </summary>
    public interface IVersion : IVersion<DateTime>
    {

    }
}