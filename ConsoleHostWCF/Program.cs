using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ConsoleHostWCF
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WCFService)))
            {
                host.AddServiceEndpoint(typeof(IWCFService), new WSHttpBinding(), "http://127.0.0.1:8081/WCFService");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    var behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://127.0.0.1:8081/WCFService/metadata");

                    host.Description.Behaviors.Add(behavior);
                }

                host.Opened += (sender, e) => { Console.WriteLine("Opened"); };
                host.Open();

                Console.Read();
            }
        }
    }
}