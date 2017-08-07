using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace FirebirdDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbFile = @"Data\master.db";
            FbConnectionStringBuilder connBuilder = new FbConnectionStringBuilder();
            //connBuilder.UserID = userId;//设置一个值，嵌入式版本并不验证用户名。
            connBuilder.ServerType = FbServerType.Embedded;//设置数据库类型为 嵌入式；
            connBuilder.Database = dbFile;//数据库文件的目录；

            using (FbConnection fbConn = new FbConnection(connBuilder.ConnectionString))
            {
                fbConn.Open();
                Console.WriteLine("连接成功！");

                fbConn.Close();
            }
        }
    }
}
