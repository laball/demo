using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;

namespace CoreWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWithCommanArgs(args).Run();
            //BuildKestrelWithJsonConfigFile(args).Run();
            //BuildWebHost(args).Run();
            BuildKestrelWithCode(args).Run();
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildKestrelWithCode(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:8001", "http://*:8002", "http://*:8003")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            return host;
        }

        /// <summary>
        /// 无法使用，暂时，似乎不能读到文件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildKestrelWithJsonConfigFile(string[] args)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("hosting.json", optional: true)
             .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            return host;
        }
    }
}