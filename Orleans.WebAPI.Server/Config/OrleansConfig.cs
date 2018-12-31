using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Business;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Implements;

namespace Orleans.WebAPI.Server.Config
{
    public static class OrleansConfig
    {
        public static IServiceCollection UseOrleans(this IServiceCollection services)
        {
            services.AddSingleton(BuildSiloHost());
            return services;
        }

        public static void ConfigOrleans(
            this IApplicationBuilder app,
            IApplicationLifetime applicationLifetime,
            IServiceProvider serviceProvider)
        {
            applicationLifetime.ApplicationStarted.Register(() => serviceProvider.GetService<ISiloHost>().StartAsync().Wait());
            applicationLifetime.ApplicationStopped.Register(() => serviceProvider.GetService<ISiloHost>().StopAsync().Wait());
        }

        public static ISiloHost BuildSiloHost()
        {
            var builder = new SiloHostBuilder()
                // Use localhost clustering for a single local silo
                .UseLocalhostClustering()
                // Configure ClusterId and ServiceId
                .Configure<ClusterOptions>(options =>
                {
                    //options.ClusterId = "dev";
                    //options.ServiceId = "MyAwesomeService";
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyAwesomeOrleansService";
                })
                .ConfigureServices((context, svr) =>
                {
                    //同理客户端（ClientBuilder），服务端，也是自己有一个ServiceCollection容器对象
                    //此处注入，处理的是针对Grain实现类的依赖注入；
                    svr.AddScoped<IMessageService, MessageService>();
                })
                //.ConfigureEndpoints(2222,40000)
                .ConfigureApplicationParts(parts =>
                {
                    //此处需要添加Grain实现类程序集
                    parts.AddApplicationPart(typeof(DemoService).Assembly).WithReferences();
                })
            // Configure connectivity
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                // Configure logging with any logging framework that supports Microsoft.Extensions.Logging.
                // In this particular case it logs using the Microsoft.Extensions.Logging.Console package.
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            return host;
        }
    }
}
