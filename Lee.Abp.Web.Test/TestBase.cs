using Abp.TestBase;
using Castle.MicroKernel.Registration;
using Lee.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.Web.Test
{
    public class TestBase<T> : AbpIntegratedTestBase<TestModule>
    {
        protected readonly T _service;

        public TestBase()
        {
            _service = LocalIocManager.Resolve<T>();
        }

        protected override void PreInitialize()
        {
            LocalIocManager.IocContainer.Register(
               Component.For<DbContextOptions<LeeAbpDbContext>>()
                   .UsingFactoryMethod(c => LeeAbpDbContextFactory.BuildOptions())
                   .LifestyleSingleton()
               );

            base.PreInitialize();
        }
    }
}
