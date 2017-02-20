using System;
using Dapper;
using Newtonsoft.Json;
using log4net;
using System.IO;
using log4net.Repository;
using System.Threading.Tasks;

//ʹ��XmlConfiguratorAttribute����log4net�ȽϷ��㣬�����ò�����������AssemblyInfo.cs�ļ���ʹ��
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