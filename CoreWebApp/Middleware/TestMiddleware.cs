using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreWebApp.Middleware
{
    /// <summary>
    /// ASP.NET Core 中间件 see https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1
    /// 
    /// </summary>
    public class TestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Trace.WriteLine($"TestMiddleware {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")} called.");

            await next.Invoke(context);
        }
    }
}
