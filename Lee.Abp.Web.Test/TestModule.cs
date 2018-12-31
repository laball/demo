using Abp.Dapper;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.TestBase;
using Lee.Abp.Application;
using Lee.Abp.Core;
using Lee.Abp.EntityFrameworkCore;

namespace Lee.Abp.Web.Test
{
    [DependsOn(
        typeof(LeeAbpApplicationModule),
        typeof(LeeAbpEntityFrameworkCoreModule),
        typeof(AbpTestBaseModule),
        typeof(LeeAbpCoreModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpDapperModule)
    )]
    public class TestModule : AbpModule
    {
    }
}
