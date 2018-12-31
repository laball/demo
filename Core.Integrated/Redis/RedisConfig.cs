using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    public static class RedisConfig
    {
        public static IServiceCollection UseRedis(this IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379";
                option.InstanceName = "";
            });

            return services;
        }
    }
}
