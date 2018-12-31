using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    /// <summary>
    /// AutoMapper配置
    /// 映射DTO到MO，或MO到DTO
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// AutoMapper注册
        /// </summary>
        public static IServiceCollection UseAbpAutoMapper(this IServiceCollection services,params Assembly[] assemblies)
        {
            Mapper.Initialize(config =>
            {
                config.AbpAutoMap(assemblies);
            });

            //使用ABP框架后，不能开启盖校验，原因是一旦有没有映射的字段，会立即弹出有异常
            //Mapper.AssertConfigurationIsValid();

            return services;
        }
    }
}