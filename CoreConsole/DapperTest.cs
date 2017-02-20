using System;
using System.Data.SqlClient;
using Dapper;
using log4net;

namespace CoreConsole
{
    public static class DapperTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DapperTest));

        public const string ConnectionString = "server='localhost';database='tese';uid='sa';pwd ='libo8923052'";

        public  static void Test()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var sql = "select * FROM Teacher";
                    var people = connection.Query<People>(sql);
                }
            }
            catch (Exception ex)
            {
                log.Error("Dappper Test Error.",ex);
            }
        }
    }

    public class People
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
