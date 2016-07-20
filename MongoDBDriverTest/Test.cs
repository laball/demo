using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriverTest
{
    [TestClass]
    public class Test
    {
        public const string ConnectionString = "mongodb://localhost";
        public const string DataBaseName = "mgdb";
        public const string CollectionName = "mgdbCustomer";
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase MongoDatabase { get; set; }
        public IMongoCollection<Customer> MongoCollection { get; set; }

        [TestInitialize()]
        public virtual void TestInitialize()
        {
            MongoClient = new MongoClient(ConnectionString);

            //添加数据时需要打开
            //MongoClient.DropDatabase(DataBaseName);

            MongoDatabase = MongoClient.GetDatabase(DataBaseName);
            MongoCollection = MongoDatabase.GetCollection<Customer>(CollectionName);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //MongoCollection.Remove(new { },true);

            var times = 100000;

            var rd = new Random();

            var time = DateTime.Now;

            for(int i = 0; i < times; i++)
            {
                var customer = new Customer
                {
                    ID = i,
                    Name = "Test_" + i.ToString("00000000"),
                    Info = new CustomerInfo
                    {
                        Phone = "1590086" + rd.Next(1000,9999).ToString(),
                        Address = "上海市南京西路1256号"
                    },
                    Orders = new List<Order>(new Order[] { new Order { ID = 11,Name = "order11",BuyTime = DateTime.Now },new Order { ID = 12,Name = "order12",BuyTime = DateTime.Now } })
                };

                MongoCollection.InsertOne(customer);
            }

            Trace.WriteLine(string.Format("Insert {0} data cost:{1} s",times.ToString(),DateTime.Now.Subtract(time).TotalSeconds.ToString("0.000")));
        }

        [TestMethod]
        public void AsQueryableTest()
        {
            var enumerable = MongoCollection.AsQueryable();
            var selectItems = from c in enumerable
                              select c;

            var count = selectItems.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void CountTest()
        {
            var count = MongoCollection.Count(FilterDefinition<Customer>.Empty);
            Assert.IsTrue(count > 0);

            count = MongoCollection.Count(c => c.Info != null);
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void CountAsyncTest()
        {
            var count = MongoCollection.CountAsync(FilterDefinition<Customer>.Empty);
            Assert.IsTrue(count.Result > 0);

            count = MongoCollection.CountAsync(c => c.Info != null);
            Assert.IsTrue(count.Result > 0);


        }

        [TestMethod]
        public void FindTest()
        {
            var one = MongoCollection.Find(FilterDefinition<Customer>.Empty).FirstOrDefault();
            Assert.IsNotNull(one);
        }



        [TestCleanup()]
        public void TestCleanup()
        {
        }

    }

}
