using Abp.Dapper;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Lee.Abp.Core
{
    /// <summary>
    /// 集成 Dapper
    /// http://www.cnblogs.com/smileberry/p/7929362.html
    /// 查看ABP源代码发现AbpDapperModule依赖于AbpEntityFrameworkModule，似乎不太合理；
    /// </summary>
    [DependsOn(typeof(AbpEntityFrameworkCoreModule),typeof(AbpDapperModule))]
    public class LeeAbpCoreModule : AbpModule
    {
        public override void PreInitialize()
        {

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LeeAbpCoreModule).GetAssembly());
        }
    }
}
