using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Interfaces;

namespace Orleans.Client
{
    public class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection()
               .AddLogging(c => c.AddConsole())
               .UseOrleans();

                var serviceProvider = services.BuildServiceProvider();

                var client = serviceProvider.GetService<IClusterClient>();
                client.Connect().Wait();

                var demoServce = client.GetGrain<IDemoService>(0);
                var ddd = demoServce.SayHello().Result.Value;
                Trace.WriteLine("result: " + ddd);
                Console.WriteLine("result: " + ddd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                Trace.WriteLine("error message: " + ex.Message + ex.StackTrace);
            }

            Console.ReadLine();

            return 0;
        }
    }
}
