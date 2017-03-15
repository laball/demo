using System.IO;
using log4net;
using Microsoft.Extensions.Configuration;

namespace CoreConsole
{
    public static class ConfigurationTest
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

        public static void XmlTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config");

            var configuration = builder.Build();
            var secton = configuration.GetSection("configuration");
            var secton2 =  secton.GetSection("appSettings");

            log.InfoFormat("key_1:{0}", secton["key_1"]);
            //log.InfoFormat("ConnectionString:{0}", configuration["ConnectionString"]);
        }

    }
}