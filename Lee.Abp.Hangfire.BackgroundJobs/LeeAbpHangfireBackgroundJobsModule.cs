using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;

namespace Lee.Abp.Hangfire.BackgroundJobs
{
    [DependsOn(typeof(AbpHangfireAspNetCoreModule))]
    public class LeeAbpHangfireBackgroundJobsModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.UseHangfire();
        }
    }
}
