using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    class Program
    {

        public const string ConnectionString = "mongodb://localhost";
        public const string DataBaseName = "mgdb";
        public const string CollectionName = "mgdbCustomer";
        public static MongoClient MongoClient { get; set; }
        public static IMongoDatabase MongoDatabase { get; set; }
        public static IMongoCollection<Customer> MongoCollection { get; set; }

        static void Main(string[] args)
        {
            MongoClient = new MongoClient(ConnectionString);
            //添加数据时需要打开
            //MongoClient.DropDatabase(DataBaseName);
            MongoDatabase = MongoClient.GetDatabase(DataBaseName);
            MongoCollection = MongoDatabase.GetCollection<Customer>(CollectionName);

            var count = MongoCollection.Count(FilterDefinition<Customer>.Empty);

            //MongoCollection.DeleteMany(FilterDefinition<Customer>.Empty);
            //return;

            //var customers = MongoCollection.FindOneAndUpdate(FilterDefinition<Customer>.Empty, Builders<Customer>.Update.Inc(x => x.Name, "Lee"));

            TaskInsertTest();

            Console.ReadLine();
        }

        static void TaskInsertTest()
        {
            IPressureTest pressure = new TaskBasedPressureTest()
            {
                ThreadCount = 10,
                InsertCount = 100000,
                Mode = InsertMode.BulkWrite,
                BulkSize = 100,
                MongoCollection = MongoCollection
            };

            pressure.Run();
        }

    }
}
