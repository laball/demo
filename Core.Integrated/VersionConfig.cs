using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    public static class VersionConfig
    {
        public static IServiceCollection EnableVersion(this IServiceCollection services)
        {
            //启用API版本
            //see https://www.hanselman.com/blog/ASPNETCoreRESTfulWebAPIVersioningMadeEasy.aspx
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 1);
                //c.ApiVersionReader = new HeaderApiVersionReader("apiVersion");
                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            return services;
        }
    }
}