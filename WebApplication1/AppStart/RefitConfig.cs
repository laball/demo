using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using WebApplication1.Refit;

namespace WebApplication1.AppStart
{
    /// <summary>
    /// Refit集成设置类
    /// </summary>
    public static class RefitConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseRefit(this IServiceCollection services)
        {
            //使用方式1：简单注入
            //此处如使用负载均衡器，则需要在每次调用时获取地址，类似及将.net core集成到SpringCloud中
            //services.AddScoped(c => RestService.For<IWharehousePositionService>("http://10.47.138.28:8080"));

            //使用方式2：自定义授权
            services.AddScoped(c => RestService.For<IWharehousePositionService>(new HttpClient(new AuthenticatedHttpClientHandler(BuildToken)) { BaseAddress = new Uri("http://10.47.138.28:8080") }));
            return services;
        }

        private static Task<string> BuildToken()
        {
            var token = "Basic YWRtaW5ANTk4MzY0ODNjODBiNzk5YzExZmU0ZmQyM2VmYjhjNjhAc3VuaW5nJCVed2Nz";
            return Task.FromResult(token);
        }
    }
}
