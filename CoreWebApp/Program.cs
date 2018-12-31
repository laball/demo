using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CoreWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    //此处可对Kestrel进行设置；
                })
                .UseStartup<Startup>();
    }
}
