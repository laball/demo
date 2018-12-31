using System.Reflection;
using Core.Dao;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Core.Integrated
{
    public static class NHibernateConfig
    {
        public static IServiceCollection UseNHibernate(this IServiceCollection services)
        {
            services.AddSingleton(c => BuildSessionFactory());
            services.AddScoped(c => c.GetService<ISessionFactory>().OpenSession());
            services.AddScoped(typeof(IRepository<>), typeof(NHRepository<>));

            return services;
        }

        private static ISessionFactory BuildSessionFactory()
        {
            //***********************************************************************
            //mapping by code
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var config = new Configuration().Configure();

            config.AddMapping(mapping);
            //***********************************************************************

            //***********************************************************************
            //mapping by hbm xml
            //var nhConfig = new Configuration().Configure();
            //***********************************************************************

            return config.BuildSessionFactory();
        }

    }
}
