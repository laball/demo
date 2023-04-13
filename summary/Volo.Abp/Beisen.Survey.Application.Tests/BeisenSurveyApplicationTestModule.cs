using Beisen.Survey.Domain.Tests;   
using Volo.Abp.Modularity;

namespace Beisen.Survey.Application.Tests
{
    [DependsOn(
        typeof(SurveyApplicationModule),
        typeof(BeisenSurveyDomainTestModule)
    )]
    public class BeisenSurveyApplicationTestModule : AbpModule
    {

    }
}