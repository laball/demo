using System;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.Log4NetIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    /// <summary>
    /// see
    /// https://stackoverflow.com/questions/47954136/masstransit-and-net-core-di-how-to-resolve-dependencies-with-parameterless-co
    /// https://aspnetmonsters.com/2017/03/2017-03-24-masstransit1/
    /// </summary>
    public static class MassTransitConfig
    {
        /// <summary>
        /// 使用默认提供的方式注入消费者
        /// 需要逐个添加较为繁琐
        /// </summary>
        /// <param name="services"></param>
        public static void UseMassTransit(this IServiceCollection services)
        {
            services.AddSingleton(c => BuildBusControl(c));
            services.AddMassTransit(c =>
            {
                //c.AddConsumer<DemoMessageConsumer>();
                //c.AddConsumer<CreateUserMessageConsumer>();
            });
        }

        /// <summary>
        /// 使用扩展后的方式注入消费者
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection UseMassTransitEx(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton(c => BuildBusControl(c));
            services.AddMassTransitEx(c =>
            {
                foreach (var assembly in assemblies)
                {
                    c.AddConsumer(assembly);
                    c.AddSaga(assembly);
                }
            });

            return services;
        }

        public static void AddMassTransitEx(this IServiceCollection serviceCollection, Action<IServiceCollectionConfiguratorEx> configure = null)
        {
            var implementationInstance = new ServiceCollectionConfiguratorEx(serviceCollection);
            configure?.Invoke(implementationInstance);
            serviceCollection.AddSingleton(implementationInstance);
        }

        /// <summary>
        /// 设置MassTransit，绑定应用程序生命周期事件进行启动和停止
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="applicationLifetime"></param>
        public static void ConfigMassTransit(this IApplicationBuilder app, IServiceProvider serviceProvider, IApplicationLifetime applicationLifetime)
        {
            var bus = serviceProvider.GetService<IBusControl>();
            applicationLifetime.ApplicationStarted.Register(bus.Start);
            applicationLifetime.ApplicationStopped.Register(bus.Stop);
        }

        /// <summary>
        /// 构建BusControl
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static IBusControl BuildBusControl(IServiceProvider serviceProvider)
        {
            return Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.UseLog4Net();//集成log4net日志
                config.UseRetry(c => c.Immediate(3));//重试策略

                var host = config.Host("localhost", "/", h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                    h.Heartbeat(5);
                });

                //此处有待优化，最好是跟不同的消费者做一个配置，避免写死在代码中；
                config.ReceiveEndpoint(host, "rbmq_test", cfg =>
                {
                    cfg.UseRetry(c => c.Immediate(3));
                    cfg.LoadFrom(serviceProvider);
                });

                config.ReceiveEndpoint(host, "rbmq_test_1", cfg =>
                {
                    cfg.LoadFrom(serviceProvider);
                });
            });
        }
    }
}