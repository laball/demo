using System.IO;
using log4net;
using Microsoft.Extensions.Configuration;

namespace CoreConsole
{
    public class ConfigurationTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationTest));

        public static void JsonTest()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("App.json");

            var configuration = builder.Build();

            log.InfoFormat("Name:{0}", configuration["Name"]);
            log.InfoFormat("ConnectionString:{0}", configuration["ConnectionString"]);
        }
    }
}
