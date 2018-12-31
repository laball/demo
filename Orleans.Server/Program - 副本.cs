using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Implements;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Orleans.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = StartSilo().Result;
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                host.StopAsync().Wait();

                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                host.StopAsync().Wait();


                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        private static async Task<ISiloHost> StartSilo()
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
                //.ConfigureEndpoints(2222,40000)
                .ConfigureApplicationParts(parts=> {
                    parts.AddApplicationPart(typeof(DemoService).Assembly).WithReferences();
                })
            // Configure connectivity
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                // Configure logging with any logging framework that supports Microsoft.Extensions.Logging.
                // In this particular case it logs using the Microsoft.Extensions.Logging.Console package.
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }

    }
}
