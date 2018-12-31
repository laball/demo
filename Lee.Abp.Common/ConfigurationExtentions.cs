using Microsoft.Extensions.Configuration;

namespace Lee.Abp.Common
{
    /// <summary>
    /// 配置扩展方法，针对单个配置项，统一在此处读取，防止Key值多处写死
    /// 复杂配置参见：https://weblog.west-wind.com/posts/2016/May/23/Strongly-Typed-Configuration-Settings-in-ASPNET-Core
    /// </summary>
    public static class ConfigurationExtentions
    {
        private const string ConnectionStringKey = "ConnectionStrings:DefaultConnection";

        public static string GetConnectionString(this IConfiguration config)
        {
            return config[ConnectionStringKey];
        }
    }
}
