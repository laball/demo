using log4net;

namespace CoreConsole
{
    public static class log4netTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(log4netTest));

        public static void Test()
        {
            log.Info("log4net start");
        }
    }
}
