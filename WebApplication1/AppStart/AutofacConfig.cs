using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1.AppStart
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider UseAutofac(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //注册Service
            builder.RegisterAssemblyTypes(typeof(DemoService).Assembly)
                .Where(c => c.Name.EndsWith("Service"))
                //.AsSelf()
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors();

            //优先使用.net core原生IOC注入，代码较为简练
            //builder.UseEF(config);
            //builder.UseNHibernate();

            //注册拦截器
            //see https://www.cnblogs.com/stulzq/p/8547839.html
            //see http://autofac.readthedocs.io/en/latest/advanced/interceptors.html
            builder.RegisterAssemblyTypes(typeof(DemoService).Assembly)
                .Where(c => c.Name.EndsWith("Interceptor"))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            //see http://docs.autofac.org/en/latest/integration/aspnetcore.html
            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());
        }
    }
}