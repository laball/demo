using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerConnectionTest
{
    class Program
    {

        public const string ConnectionString = "server='HZSWVDSQL01';database='HzHmisDev';uid='apidev';pwd ='haozhuo2015'";

        static void Main(string[] args)
        {
            int maxCount = 1000;
            List<SqlConnection> collection = new List<SqlConnection>();
            for (int i = 0; i < maxCount; i++)
            {
                Console.WriteLine(string.Format("成功创建连接对象{0}", i));
                var db = new SqlConnection(ConnectionString);
                db.Open();

                collection.Add(db);
            }
        }
    }
}
