using Beisen.Survey.Domain.Shared;
using Volo.Abp.Modularity;

namespace Beisen.Survey.Domain
{
    [DependsOn(typeof(SurveyDomainSharedModule))]
    public class SurveyDomainModule : AbpModule
    {

    }
}