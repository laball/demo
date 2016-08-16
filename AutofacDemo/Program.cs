using Autofac;
using Autofac.Extras.DynamicProxy;
using System.Diagnostics;

namespace AutofacDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestDao>()
                   .As<ITestDao>()
                   //.EnableClassInterceptors();
                   .EnableInterfaceInterceptors();

            builder.RegisterModule<InterceptorModule>();

            var container = builder.Build();
            var willBeIntercepted = container.Resolve<ITestDao>();
            var ddd = willBeIntercepted.GetDefaultDept();

            Trace.WriteLine(ddd);
        }
    }
}
