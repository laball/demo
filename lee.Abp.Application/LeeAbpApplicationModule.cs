using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Lee.Abp.Core;

namespace Lee.Abp.Application
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(LeeAbpCoreModule), typeof(AbpAutoMapperModule))]
    public class LeeAbpApplicationModule : AbpModule
    {
        public override void PostInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                //config.CreateMap<>();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LeeAbpApplicationModule).GetAssembly());
        }
    }
}
