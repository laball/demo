﻿using System;
using System.Diagnostics;
using System.Threading;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    internal class Program
    {
        public const string ConnectionString = "mongodb://admin:a1234567@10.50.50.24:27017";
        public const string DataBaseName = "admin";
        public const string CollectionName = "ReportEntity";
        public static MongoClient MongoClient { get; set; }
        public static IMongoDatabase MongoDatabase { get; set; }
        public static IMongoCollection<ReportEntity> MongoCollection { get; set; }

        private static void Main(string[] args)
        {
            MongoClient = new MongoClient(ConnectionString);
            //添加数据时需要打开
            //MongoClient.DropDatabase(DataBaseName);
            MongoDatabase = MongoClient.GetDatabase(DataBaseName);
            MongoCollection = MongoDatabase.GetCollection<ReportEntity>(CollectionName);

            var item = MongoCollection.Find<ReportEntity>(c => c.WorkNo == "00308350").FirstOrDefault();
            item.OrderName = "pt_dev";

            int start = 3920000 + 70000;
            int end = 400 * 10000 + 70000;

            while (start < end)
            {
                item._id = MongoDB.Bson.ObjectId.GenerateNewId();
                item.WorkNo = (start++).ToString();

                MongoCollection.InsertOne(item);

                if ((start % 500) == 0)
                {
                    Console.WriteLine(start);

                    Thread.Sleep(500);
                }
            }

            var count = MongoCollection.Count(FilterDefinition<ReportEntity>.Empty);
            Trace.WriteLine(count);

            //MongoCollection.DeleteMany(FilterDefinition<Customer>.Empty);
            //return;

            //var customers = MongoCollection.FindOneAndUpdate(FilterDefinition<Customer>.Empty, Builders<Customer>.Update.Inc(x => x.Name, "Lee"));

            //TaskInsertTest();

            Console.ReadLine();
        }

        private static void TaskInsertTest()
        {
            //pressure.Run();
        }
    }
}