using System;
using System.Reflection;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Core.Integrated
{
    /// <summary>
    /// 扩展<see cref="IServiceCollectionConfigurator"/>支持以<see cref="System.Type"/>和<see cref="Assembly"/>程序集形式注册消费
    /// </summary>
    public interface IServiceCollectionConfiguratorEx : IServiceCollectionConfigurator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        void AddConsumer(Type type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        void AddConsumer(Assembly assembly);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        void AddSaga(Type type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        void AddSaga(Assembly assembly);
    }
}
