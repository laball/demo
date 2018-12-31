using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Business;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Implements;
using Orleans.Runtime.Configuration;

namespace Orleans.Server
{
    public static class OrleansConfig
    {
        public static IServiceCollection UseOrleans(this IServiceCollection services)
        {
            services.AddSingleton(BuildSiloHost());
            return services;
        }

        public static ISiloHost BuildSiloHost()
        {
            var ddd = new MySql.Data.MySqlClient.MySqlCommand();

            var config = new ClusterConfiguration();
            config.StandardLoad();

            var builder = new SiloHostBuilder();
            //builder.LoadClusterConfiguration()
            builder.UseConfiguration(config)
                .ConfigureLogging(logging => logging.AddLog4Net("log4net.config"))
                .ConfigureServices((context, svr) =>
                {
                    //同理客户端（ClientBuilder），服务端，也是自己有一个ServiceCollection容器对象
                    //此处注入，处理的是针对Grain实现类的依赖注入；
                    svr.AddScoped<IMessageService, MessageService>();
                })
                .ConfigureApplicationParts(parts =>
                {
                    //此处需要添加Grain实现类程序集
                    parts.AddApplicationPart(typeof(DemoService).Assembly).WithReferences();
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyAwesomeOrleansService";
                });

            return builder.Build();
        }

        public static ISiloHost BuildSiloHost1()
        {
            //所有Options列表
            //http://dotnet.github.io/orleans/Documentation/Deployment-and-Operations/Configuration-Guide/Options-List.html

            var builder = new SiloHostBuilder()

                // Use localhost clustering for a single local silo
                .UseLocalhostClustering(9001, 9999)
                  .ConfigureServices((context, svr) =>
                  {
                      //同理客户端（ClientBuilder），服务端，也是自己有一个ServiceCollection容器对象
                      //此处注入，处理的是针对Grain实现类的依赖注入；
                      svr.AddScoped<IMessageService, MessageService>();
                  })
                // Configure ClusterId and ServiceId
                .Configure<ClusterOptions>(options =>
                {
                    //options.ClusterId = "dev";
                    //options.ServiceId = "MyAwesomeService";
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyAwesomeOrleansService";
                })

                //Unable to cast object of type 'MySql.Data.Types.MySqlDateTime' to type 'System.Nullable`1[System.DateTime]'.
                //连接字符串添加：convert zero datetime=True;

                //Table 'test.orleansquery' doesn't exist
                /*
                 * CREATE TABLE `NewTable` (
                       `ID`  int(11) NOT NULL AUTO_INCREMENT ,
                       `QueryKey`  varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
                       `QueryText`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
                       PRIMARY KEY (`ID`)
                       )
                */

                //.UseAdoNetClustering(options =>
                //{
                //    options.ConnectionString = "Data Source=10.27.225.165;port=3306;Initial Catalog=emms_dev_temp;user id=wcsuser;password=wcsuser;charset=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=none;convert zero datetime=True;";
                //    options.Invariant = "MySql.Data.MySqlClient";
                //})

                //.ConfigureEndpoints(9001, 9999)

                .ConfigureServices((context, svr) =>
                {
                    //同理客户端（ClientBuilder），服务端，也是自己有一个ServiceCollection容器对象
                    //此处注入，处理的是针对Grain实现类的依赖注入；
                    svr.AddScoped<IMessageService, MessageService>();
                })

                .ConfigureApplicationParts(parts =>
                {
                    //此处需要添加Grain实现类程序集
                    parts.AddApplicationPart(typeof(DemoService).Assembly).WithReferences();
                })
                // Configure connectivity
                //.Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                // Configure logging with any logging framework that supports Microsoft.Extensions.Logging.
                // In this particular case it logs using the Microsoft.Extensions.Logging.Console package.
                .ConfigureLogging(logging => logging.AddLog4Net("log4net.config"));

            var host = builder.Build();
            return host;
        }



        public static ISiloHost BuildSiloHost2()
        {
            //所有Options列表
            //http://dotnet.github.io/orleans/Documentation/Deployment-and-Operations/Configuration-Guide/Options-List.html

            var builder = new SiloHostBuilder()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "my-first-cluster";
                        options.ServiceId = "MyAwesomeOrleansService";
                    })
                    .UseAdoNetClustering(options =>
                    {
                        options.ConnectionString = "Data Source=10.27.225.165;port=3306;Initial Catalog=emms_dev_temp;user id=wcsuser;password=wcsuser;charset=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=none;convert zero datetime=True;";
                        options.Invariant = "MySql.Data.MySqlClient";
                    })
                    .ConfigureEndpoints(9001, 9999)
                    .ConfigureLogging(logging => logging.AddLog4Net("log4net.config"))
                    .Build();

            return builder;
        }
    }
}
