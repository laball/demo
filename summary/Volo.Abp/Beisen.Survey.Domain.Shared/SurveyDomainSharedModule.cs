using System.Diagnostics;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Beisen.Survey.Domain.Shared
{
    public class SurveyDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Trace.WriteLine($"1-PreConfigureServices");

            // 关闭自动注册
            //SkipAutoServiceRegistration = true;
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Trace.WriteLine($"2-ConfigureServices");
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Trace.WriteLine($"3-PostConfigureServices");
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            Trace.WriteLine($"4-OnPreApplicationInitialization");
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            Trace.WriteLine($"5-OnApplicationInitialization");
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            Trace.WriteLine($"6-OnPostApplicationInitialization");
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            Trace.WriteLine($"7-OnApplicationShutdown");
        }
    }
}