using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Orleans.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            



        }


        /*
        var builder = new ClientBuilder()
                // Use localhost clustering for a single local silo
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts=> {

                    parts.AddApplicationPart(typeof(IDemoService).Assembly).WithReferences();

                })
                // Configure ClusterId and ServiceId
                .Configure<ClusterOptions>(options =>
                {
                    //options.ClusterId = "dev";
                    //options.ServiceId = "MyAwesomeService";
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyAwesomeOrleansService";
                })
                .ConfigureLogging(logging => logging.AddConsole());

            var client = builder.Build();



            client.Connect().Wait();            

            var demoServce = client.GetGrain<IDemoService>(0);

            var ddd = demoServce.SayHello();
         */


    }
}
