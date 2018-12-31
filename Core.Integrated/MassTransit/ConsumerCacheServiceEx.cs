using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Core.Integrated
{
    /// <summary>
    /// <see cref="IConsumerCacheService"/>接口实现
    /// 原实现存在Bug
    /// </summary>
    public class ConsumerCacheServiceEx : IConsumerCacheService
    {
        readonly ConcurrentDictionary<Type, ICachedConfigurator> _configurators = new ConcurrentDictionary<Type, ICachedConfigurator>();

        public void Add<T>() where T : class, IConsumer
        {
            _configurators.GetOrAdd(typeof(T), _ => new ConsumerCachedConfigurator<T>());
        }

        public IEnumerable<ICachedConfigurator> GetConfigurators()
        {
            return _configurators.Values.ToList();
        }

        public void Add(Type consumerType)
        {
            _configurators.GetOrAdd(consumerType, _ => (ICachedConfigurator)Activator.CreateInstance(typeof(ConsumerCachedConfigurator<>).MakeGenericType(consumerType)));
        }
    }
}
