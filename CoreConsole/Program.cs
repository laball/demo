using System;
using Dapper;
using Newtonsoft.Json;

namespace CoreConsole
{
    public class Program
    {
        public const string ConnectionString = "server='Lee';database='tese';uid='sa';pwd ='@LiBo#8923052'";

        public static void Main(string[] args)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var sql = "select * FROM People";

                    var people = connection.Query<People>(sql);

                    foreach (var person in people)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(person));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            Console.ReadLine();
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