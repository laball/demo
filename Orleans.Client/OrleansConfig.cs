using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Interfaces;
using Orleans.Runtime.Configuration;

namespace Orleans.Client
{
    public static class OrleansConfig
    {
        public static IServiceCollection UseOrleans(this IServiceCollection services)
        {
            //注入Orleans客户端操作接口，可从中获取Grain接口；

            //使用默认配置文件
            services.AddSingleton(BuildOrleansClient());
            //使用指定配置文件
            //services.AddSingleton(BuildOrleansClient("ClientConfiguration.xml"));

            return services;
        }

        private static IClusterClient BuildOrleansClient()
        {
            //默认配置文件："ClientConfiguration.xml", "OrleansClientConfiguration.xml", "Client.config", "Client.xml"
            IClusterClient client = new ClientBuilder()
                .LoadConfiguration()//加载默认配置文件；
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyAwesomeOrleansService";
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddLog4Net("log4net.config");
                })
                .Build();

            return client;
        }

        private static IClusterClient BuildOrleansClient(string configFile)
        {
            IClusterClient client = new ClientBuilder()
                .UseConfiguration(ClientConfiguration.LoadFromFile(configFile))
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyAwesomeOrleansService";
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddLog4Net("log4net.config");
                })
                .Build();

            return client;
        }


        private static IClusterClient BuildOrleansClient1()
        {
            var builder = new ClientBuilder()
              // Use localhost clustering for a single local silo

              .UseLocalhostClustering(9999)
              .ConfigureApplicationParts(parts =>
              {
                  parts.AddApplicationPart(typeof(IDemoService).Assembly).WithReferences();
              })
              .ConfigureServices((context, services) =>
              {
                  //ClientBuilder通过内部类ServiceProviderBuilder构建DI，是一个私有的ServiceCollection
                  //因此，此处的DI扩展，其实是针对内部容器的DI处理
                  //个人感觉，在默认使用构造函数注入的模式下，客户端使用的接口，应该没有注入的必要，
                  //服务端，针对接口实现，可以注入实现类所依赖的资源
              })
              .Configure<ClusterOptions>(options =>
              {
                  // Configure ClusterId and ServiceId
                  //options.ClusterId = "dev";
                  //options.ServiceId = "MyAwesomeService";
                  options.ClusterId = "my-first-cluster";
                  options.ServiceId = "MyAwesomeOrleansService";
              })
              .ConfigureLogging(logging => logging.AddLog4Net("log4net.config"));

            return builder.Build();
        }
    }
}
