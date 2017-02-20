using Autofac;
using log4net;

namespace CoreConsole
{
    public class AutofacTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AutofacTest));

        public static void Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(typeof(TestImp))
                   .As<ITest>();

            using (var container = builder.Build())
            {
                ITest test = container.Resolve<ITest>();
                test.Test();
            }
        }
    }

    public interface ITest
    {
        void Test();
    }


    public class TestImp : ITest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TestImp));

        public void Test()
        {
            log.Info("TestImp.Test Called.");
        }
    }

}
