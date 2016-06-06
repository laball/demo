using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.Attributes;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace NHibernateDemo
{
    class Program
    {
        static ISessionFactory SessionFactory;

        static void Main(string[] args)
        {
            MappingByAttribute();


            var session = SessionFactory.OpenSession();

            var logs = session.QueryOver<Needredorequest>().List();


            //var bigData = new StreamReader(File.OpenRead("test.txt"),Encoding.Unicode).ReadToEnd();
            var bigData = new StreamReader(File.OpenRead("test.txt")).ReadToEnd();

            var redo = new Needredorequest
            {
                Url = "http://www.baidu.com",
                Postdata = "各个个热个个个",
                Redocount = 0,
                Issuccess = false,
                CreateOn = DateTime.Now,
                LastRedo = DateTime.Now
            };

            session.Save(redo);
            session.Flush();



            //var items = session.QueryOver<Doctor>().Take(5).List();

            //var sql = @"select count(*) from DOCTOR";
            //var count = session.CreateSQLQuery(sql).List();

        }


        static void MappingByCode()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();

            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(DoctorByCodeMapping));

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            //输出编译好的XML映射
            Trace.WriteLine(mapping.AsString());

            cfg.AddMapping(mapping);

            SessionFactory = cfg.BuildSessionFactory();
        }

        static void FluentMapping()
        {
            SessionFactory = Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"server=HZSWVDSQL01;database=tese;uid=sa;pwd=libo8923052;").ShowSql())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Doctor>())
              .ExposeConfiguration(cfg => new SchemaExport(cfg))
              .BuildSessionFactory();
        }


        static void MappingByAttribute()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();

            //NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
            cfg.AddInputStream(HbmSerializer.Default.Serialize(Assembly.GetExecutingAssembly()));

            SessionFactory = cfg.BuildSessionFactory();
        }

        static void MappingByXML()
        {

        }
    }
}
