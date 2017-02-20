using System;
using System.Data.SqlClient;
using log4net;

namespace CoreConsole
{
    public static class MSSQLServerTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MSSQLServerTest));

        public const string ConnectionString = "server='localhost';database='tese';uid='sa';pwd ='libo8923052'";

        public static void Test()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var sql = "select * FROM Teacher";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        var count = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("MS SQL Test Error.", ex);
            }
        }
    }
}
