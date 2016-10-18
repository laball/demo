using System;
using System.Linq;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutofacDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RegistByCode();
            //RegisByAssembly();
            //BuildByXml();
            //BuildByJson();

            using (var scope = container.BeginLifetimeScope())
            {
                var fly = scope.Resolve<IFly>();
                fly.Fly();
            }

            Console.ReadLine();
        }

        private static void RegistByCode()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Bird>().As<IFly>();
            builder.RegisterType<Dog>().As<IRun>();

            container = builder.Build();
        }

        private static void RegisByAssembly()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(c => c.Name.EndsWith("Bird"))
                .AsImplementedInterfaces();

            container = builder.Build();
        }

        private static void BuildByXml()
        {
            var config = new ConfigurationBuilder();
            config.AddXmlFile("autofac.xml");//need Microsoft.Extensions.Configuration.Xml

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            container = builder.Build();
        }

        private static void BuildByJson()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");//need Microsoft.Extensions.Configuration.Json

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            container = builder.Build();
        }

        private static IContainer container;
    }
}