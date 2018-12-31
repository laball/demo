using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Interfaces;

namespace Orleans.WebAPI.Client.Config
{
    public static class OrleansConfig
    {
        public static IServiceCollection UseOrleans(this IServiceCollection services)
        {
            //注入Orleans客户端操作接口，可从中获取Grain接口；
            services.AddSingleton(BuildOrleansClient());

            //将Orleans容器中的Grain接口注入到外部容器中
            //不建议采用这种方式，此种方式有悖Actor Model的设计理念
            //services.AddScoped(c => c.GetService<IClusterClient>().GetGrain<IDemoService>(0));

            return services;
        }

        public static void ConfigOrleans(this IApplicationBuilder app, IApplicationLifetime applicationLifetime, IServiceProvider serviceProvider)
        {
            applicationLifetime.ApplicationStarted.Register(() => serviceProvider.GetService<IClusterClient>().Connect().Wait());
            applicationLifetime.ApplicationStopped.Register(() => serviceProvider.GetService<IClusterClient>().Close().Wait());
        }

        private static IClusterClient BuildOrleansClient()
        {
            var builder = new ClientBuilder()
              // Use localhost clustering for a single local silo
              .AddClientInvokeCallback((request, grain) =>
              {
                  Trace.WriteLine($"InterfaceId: {request.InterfaceId},InterfaceVersion: {request.InterfaceVersion},MethodId: {request.MethodId},Arguments: {string.Join(",", request.Arguments ?? new object[] { })}");
              })
              .AddClusterConnectionLostHandler((sender, e) =>
              {
                  Trace.WriteLine($"client disconnection from a cluster.");
              })
              .UseLocalhostClustering()
              .ConfigureApplicationParts(parts =>
              {
                  parts.AddApplicationPart(typeof(IDemoService).Assembly).WithReferences();
              })
              .ConfigureServices((context, services) =>
              {
                  //ClientBuilder通过内部类ServiceProviderBuilder构建DI，是一个私有的ServiceCollection
                  //因此，此处的DI扩展，其实是针对内部容器的DI处理
                  //个人感觉，在默认使用构造函数注入的模式下，客户端使用的接口，应该没有注入的必要，
                  //服务端，针对接口实现，可以注入实现类所需要的依赖
              })
              .Configure<ClusterOptions>(options =>
              {
                  // Configure ClusterId and ServiceId
                  //options.ClusterId = "dev";
                  //options.ServiceId = "MyAwesomeService";
                  options.ClusterId = "my-first-cluster";
                  options.ServiceId = "MyAwesomeOrleansService";
              })
              .ConfigureLogging(logging => logging.AddConsole());

            return builder.Build();
        }
    }
}
