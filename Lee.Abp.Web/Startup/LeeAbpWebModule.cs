using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching;
using Abp.Runtime.Caching.Redis;
using Lee.Abp.Application;
using Lee.Abp.Core;
using Lee.Abp.EntityFrameworkCore;
using Lee.Abp.Hangfire.BackgroundJobs;
using Lee.Abp.Quartz.BackgroundJobs;

namespace Lee.Abp.Web.Startup
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(LeeAbpApplicationModule),
        typeof(LeeAbpEntityFrameworkCoreModule),
        typeof(LeeAbpCoreModule),
        typeof(AbpAspNetCoreModule),
        typeof(LeeAbpQuartzBackgroundJobsModule),
        typeof(LeeAbpHangfireBackgroundJobsModule),
        typeof(AbpRedisCacheModule),
        typeof(AbpAspNetCoreSignalRModule)
        )]
    public class LeeAbpWebModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
            var ttt = Configuration.MultiTenancy.Resolvers;

            base.PreInitialize();

            //*****************************************************************************************
            //关闭ABP包装接API返回结果
            //see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2451
            //或使用DontWrapResult，但无法全局注册，需要应用于每一个Controller
            Configuration.Modules.AbpAspNetCore().DefaultWrapResultAttribute.WrapOnSuccess = false;
            Configuration.Modules.AbpAspNetCore().DefaultWrapResultAttribute.WrapOnError = false;
            //*****************************************************************************************




            //******************************************************************
            //集成 abp redis
            //see http://www.cnblogs.com/1zhk/p/5389057.html
            //IocManager.Register<ICacheManager, AbpRedisCacheManager>();

            //Configuration.Caching.UseRedis(options =>
            //{

            //});
            //******************************************************************
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LeeAbpWebModule).GetAssembly());
        }
    }
}
