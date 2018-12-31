using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(AbpSignalrApi.Startup))]

namespace AbpSignalrApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Trace.WriteLine("******MapSignalR******");

            //默认方式不支持跨域
            //app.MapSignalR();

            //see:https://docs.microsoft.com/zh-cn/aspnet/signalr/overview/guide-to-the-api/hubs-api-guide-javascript-client#crossdomain

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var hubConfig = new HubConfiguration
                {

                };

                map.RunSignalR(hubConfig);
            });
        }
    }
}