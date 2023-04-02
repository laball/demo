using Beisen.Survey.Application.Contracts;
using Volo.Abp.Modularity;

namespace Beisen.Survey.HttpApi
{
    [DependsOn(typeof(SurveyApplicationContractsModule))]
    public class SurveyHttpApiModule : AbpModule
    {

    }
}