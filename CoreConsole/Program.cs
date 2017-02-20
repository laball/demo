using System;
using Dapper;
using Newtonsoft.Json;
using log4net;
using System.IO;
using log4net.Repository;
using System.Threading.Tasks;

//使用XmlConfiguratorAttribute设置log4net比较方便，该设置不仅仅可以在AssemblyInfo.cs文件中使用
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace CoreConsole
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            try
            {
                //log4netTest.Test();
                ConfigurationTest.JsonTest();
                //ConfigurationTest.XmlTest();

                //AutofacTest.Test();

            }
            catch (System.Exception ex)
            {
                log.Info(ex.StackTrace);
            }

            Console.ReadLine();
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            log.Error("Task Error.", e.Exception);
            e.SetObserved();
        }
    }
}