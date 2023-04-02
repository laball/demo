using Beisen.Survey.Domain;
using Volo.Abp.Modularity;

namespace Beisen.Survey.Application
{
    [DependsOn(typeof(SurveyDomainModule))]
    public class SurveyApplicationModule : AbpModule
    {

    }
}