using log4net;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

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
            var provider = configuration.Providers.FirstOrDefault();
            var tt = string.Empty;
            var keys = provider.TryGet("appSettings:add:key", out tt);
            var cfgSection = configuration.GetSection("configuration");
            var appSettingsSection =  cfgSection.GetSection("appSettings");
            var addSection = appSettingsSection.GetChildren();

            var ddd = configuration.GetConnectionString("key_1");

            log.InfoFormat("key_1:{0}", appSettingsSection["key_1"]);
            //log.InfoFormat("ConnectionString:{0}", configuration["ConnectionString"]);
        }

    }
}