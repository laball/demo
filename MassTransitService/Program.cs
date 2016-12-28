using log4net.Config;
using Topshelf;
using Topshelf.Logging;

namespace MassTransitService
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetLogWriterFactory.Use();
            XmlConfigurator.Configure();

            HostFactory.Run(config =>
            {
                config.Service<GreetingServer>(server =>
                {
                    server.ConstructUsing(name => new GreetingServer());
                    server.WhenStarted(s => s.Start());
                    server.WhenStopped(s => s.Stop());
                });

                config.StartAutomatically();
                config.RunAsLocalSystem();
                config.SetDescription("A greeting service");
                config.SetDisplayName("Greeting Service");
                config.SetServiceName("GreetingService");
            });
        }
    }
}
