using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    public static class CorsConfig
    {
        public static IServiceCollection EnableCors(this IServiceCollection services)
        {
            //跨域
            //see https://stackoverflow.com/questions/31942037/how-to-enable-cors-in-asp-net-core
            services.AddCors(c => c.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            return services;
        }
    }
}