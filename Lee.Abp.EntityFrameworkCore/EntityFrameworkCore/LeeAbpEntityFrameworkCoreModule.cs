using System.Reflection;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Lee.Abp.Common;
using Lee.Abp.Core;
using Microsoft.Extensions.Configuration;

namespace Lee.Abp.EntityFrameworkCore
{
    [DependsOn(typeof(LeeAbpCoreModule), typeof(AbpEntityFrameworkCoreModule))]
    public class LeeAbpEntityFrameworkCoreModule : AbpModule
    {
        private readonly IConfiguration _configuration;

        public LeeAbpEntityFrameworkCoreModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void PostInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
          
            Configuration.DefaultNameOrConnectionString = _configuration.GetConnectionString();
        }
    }
}
