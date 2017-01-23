using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://logging.apache.org/log4net/log4net-1.2.13/release/sdk/log4net.Layout.PatternLayout.html
            var log = LogManager.GetLogger("testLogger");


            log.Debug("MyClass log test");


            var ddd = DateTime.MinValue;

            var span = TimeSpan.Parse("10:10:10");

            log.Debug("log4net db test");
            log.Error("log4net db test");
            log.Fatal("log4net db test");
            log.Info("log4net db test");
            log.Warn("log4net db test");

            log.Error("log4net error test", new InvalidOperationException("InvalidOperationException"));

            //*********************************************************
            //配置中设置了bufferSize的值，则程序关闭时可能会丢掉最后的日志
            ILogger logger = log.Logger;
            ILoggerRepository logRepository = logger.Repository;
            IAppender[] apperders = logRepository.GetAppenders();
            foreach (var apperder in apperders)
            {
                var adoNetAppender = apperder as AdoNetAppender;
                if (adoNetAppender != null)
                {
                    adoNetAppender.Flush();
                }
            }
            //*********************************************************


            Console.ReadLine();
        }



        private static readonly ILog log = LogManager.GetLogger("DBLog");
    }


    public class MyClass
    {

    }


}
