using Beisen.Survey.Domain;
using Volo.Abp.Modularity;

namespace Beisen.Survey.Application.Contracts
{
    [DependsOn(typeof(SurveyDomainModule))]
    public class SurveyApplicationContractsModule : AbpModule
    {

    }
}