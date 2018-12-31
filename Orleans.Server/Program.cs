using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Hosting;

namespace Orleans.Server
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
              .AddLogging(c => c.AddConsole())
              .UseOrleans();

            var serviceProvider = services.BuildServiceProvider();

            var siloHost = serviceProvider.GetService<ISiloHost>();

            var watch = new Stopwatch();
            watch.Start();

            siloHost.StartAsync().Wait();

            watch.Stop();

            Console.WriteLine($"SiloHost start cost: {watch.Elapsed.TotalSeconds.ToString("0.000")}s");

            Console.WriteLine("SiloHost Started.");
            Console.ReadLine();           

            return 0;
        }
    }
}
