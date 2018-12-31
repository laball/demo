using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4Demo.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddIdentityServerInMemory(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())  //配置资源
                .AddInMemoryClients(Config.GetClients())        //配置客户端
                .AddTestUsers(Config.GetUsers()); //添加测试用户

            return services;
        }

        public static IServiceCollection AddIdentityServerCustomerUserPasswordCheck(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())  //配置资源
                .AddInMemoryClients(Config.GetClients())        //配置客户端
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            services.AddSingleton<TestDbContext>();

            return services;
        }
    }

}
