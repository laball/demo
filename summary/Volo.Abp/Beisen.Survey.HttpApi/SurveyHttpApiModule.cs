using Beisen.Survey.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Beisen.Survey.HttpApi
{
    [DependsOn(typeof(SurveyApplicationContractsModule))]
    public class SurveyHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // 如有自定义Controller需要执行 AddApplicationPartIfNotExists ，否则Swagger中不会显示Controller下的API；
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SurveyHttpApiModule).Assembly);
            });
        }
    }
}