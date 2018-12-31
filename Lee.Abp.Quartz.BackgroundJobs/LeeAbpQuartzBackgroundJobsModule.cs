using Abp.Modules;
using Abp.Quartz;
using Abp.Reflection.Extensions;

namespace Lee.Abp.Quartz.BackgroundJobs
{
    [DependsOn(typeof(AbpQuartzModule))]
    public class LeeAbpQuartzBackgroundJobsModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LeeAbpQuartzBackgroundJobsModule).GetAssembly());
        }
    }
}
