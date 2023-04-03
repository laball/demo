using Beisen.Survey.Domain;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Beisen.Survey.Application
{
    [DependsOn(typeof(SurveyDomainModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class SurveyApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SurveyApplicationAutoMapperProfile>();
            });
        }
    }
}