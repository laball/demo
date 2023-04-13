using Beisen.Survey.EntityFrameworkCore.Tests;
using Volo.Abp.Modularity;

namespace Beisen.Survey.Domain.Tests
{
    [DependsOn(typeof(BeisenSurveyEntityFrameworkCoreTestModule))]
    public class BeisenSurveyDomainTestModule : AbpModule
    {

    }
}