using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQLServerTest
{
    internal class Program
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MSSQL2008R2"].ConnectionString;

        private static void Main(string[] args)
        {
            int workerThreads;
            int completionPortThreads;

            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);

            ThreadPool.SetMinThreads(50, 50);

            var threadCount = 500;
            for (int i = 0; i < threadCount; i++)
            {
                Task.Factory.StartNew(Insert);
            }

            Console.ReadLine();
        }

        private static void Insert()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                var count = 10000;

                for (int i = 0; i < count; i++)
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        var cmdText = @"  INSERT INTO t_log
                                      (CODE,NAME,UNITE)
                                      VALUES(@code,@name,@unite)";

                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.Parameters.Add(new SqlParameter("@code", "code_" + threadId.ToString() + "_" + i.ToString()));
                            command.Parameters.Add(new SqlParameter("@name", "name_" + threadId.ToString() + "_" + i.ToString()));
                            command.Parameters.Add(new SqlParameter("@unite", "unite_" + threadId.ToString() + "_" + i.ToString()));

                            command.ExecuteNonQuery();
                        }

                        Thread.Sleep(50);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }

            Trace.WriteLine(string.Format("ThreadID:{0} exec end.", threadId));
        }
    }
}