using System;
using System.Linq;
using System.Reflection;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.Saga;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceCollectionConfiguratorEx : IServiceCollectionConfigurator, IServiceCollectionConfiguratorEx
    {
        private readonly IConsumerCacheService _consumerCacheService;
        private readonly ISagaCacheService _sagaCacheService;
        private readonly IServiceCollection _services;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public ServiceCollectionConfiguratorEx(IServiceCollection services)
        {
            _services = services;

            _consumerCacheService = new ConsumerCacheServiceEx();
            services.AddSingleton(_consumerCacheService);

            _sagaCacheService = new SagaCacheService();
            services.AddSingleton(_sagaCacheService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddConsumer<T>() where T : class, IConsumer
        {
            _services.AddScoped<T>();
            _consumerCacheService.Add<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void AddConsumer(Type type)
        {
            _services.AddScoped(type);
            _consumerCacheService.Add(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public void AddConsumer(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var consumerTypes = from t in assembly.GetExportedTypes()
                                where typeof(IConsumer).IsAssignableFrom(t)
                                select t;

            foreach (var type in consumerTypes)
            {
                AddConsumer(type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddSaga<T>() where T : class, ISaga
        {
            _sagaCacheService.Add<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void AddSaga(Type type)
        {
            _sagaCacheService.Add(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public void AddSaga(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var sagaTypes = from t in assembly.GetExportedTypes()
                            where typeof(ISaga).IsAssignableFrom(t)
                            select t;

            foreach (var type in sagaTypes)
            {
                AddSaga(type);
            }
        }
    }
}
