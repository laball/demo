using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB;
using MongoEntity;

namespace SamusMongoClientTest
{
    [TestClass]
    public class SamusTest
    {
        public const string ConnectionString = "mongodb://localhost";
        public const string DataBaseName = "samusdb";
        public const string CollectionName = "samusCustomer";

        public IMongo Mongo { get; set; }
        public IMongoDatabase Database { get; set; }

        public IMongoCollection<Customer> MongoCollection { get; set; }

        [TestInitialize()]
        public virtual void TestInitialize()
        {
            Mongo = new Mongo(ConnectionString);
            Mongo.Connect();

            Database = Mongo[DataBaseName];//Database = Mongo.GetDatabase(DataBaseName);
            MongoCollection = Database.GetCollection<Customer>(CollectionName);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            Mongo.Disconnect();
        }

        [TestMethod]
        public void InsertTest()
        {
            //MongoCollection.Remove(new { },true);

            var times = 100000;

            var rd = new Random();

            var time = DateTime.Now;

            for (int i = 0; i < times; i++)
            {
                var customer = new Customer
                {
                    Name = "Test_" + i.ToString("00000000"),
                    Info = new CustomerInfo
                    {
                        Phone = "1590086" + rd.Next(1000, 9999).ToString(),
                        Address = "上海市南京西路1256号"
                    },
                    Orders = new List<Order>(new Order[] { new Order { ID = 11, Name = "order11", BuyTime = DateTime.Now }, new Order { ID = 12, Name = "order12", BuyTime = DateTime.Now } })
                };

                MongoCollection.Insert(customer);
            }

            Trace.WriteLine(string.Format("Insert {0} data cost:{1} s", times.ToString(), DateTime.Now.Subtract(time).TotalSeconds.ToString("0.000")));
        }

        [TestMethod]
        public void CountTest()
        {
            var count = MongoCollection.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var items = MongoCollection.Linq().ToList();
        }
    }
}