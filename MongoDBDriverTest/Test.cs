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

            for (int i = 0; i < times; i++)
            {
                var customer = new Customer
                {
                    Age = rd.Next(12, 60),
                    CreateOn = DateTime.Now,
                    Height = 150.00d + 100 * rd.NextDouble(),
                    Info = new CustomerInfo
                    {
                        Phone = "1590086" + rd.Next(1000, 9999).ToString(),
                        Address = "上海市南京西路1256号"
                    },
                    IsMail = rd.Next(0, 1) == 0 ? false : true,
                    Name = "Test_" + i.ToString("00000000"),
                    Orders = new List<Order>(new Order[] { new Order { ID = 11, Name = "order11", BuyTime = DateTime.Now }, new Order { ID = 12, Name = "order12", BuyTime = DateTime.Now } }),
                    Weight = 48.00f + 50f * (float)rd.NextDouble()
                };

                MongoCollection.InsertOne(customer);
            }

            Trace.WriteLine(string.Format("Insert {0} data cost:{1} s", times.ToString(), DateTime.Now.Subtract(time).TotalSeconds.ToString("0.000")));
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
            var options = new FindOptions
            {
                AllowPartialResults = true,
                BatchSize = 100,
                Comment = "",
                CursorType = CursorType.Tailable,
                MaxAwaitTime = new TimeSpan(0, 0, 30),
                MaxTime = new TimeSpan(0, 0, 30)
            };

            //empty filter
            var one1 = MongoCollection.Find(FilterDefinition<Customer>.Empty).Limit(1).FirstOrDefault();
            Assert.IsNotNull(one1);

            // Age > 30
            var gt1 = MongoCollection.Find("{'Age':{$gt:30}}").Limit(1).FirstOrDefault();
            Assert.IsNotNull(gt1);
            var gt2 = MongoCollection.Find(c => c.Age > 30).Limit(1).FirstOrDefault();
            Assert.IsNotNull(gt2);

            //Age < 25
            var lt1 = MongoCollection.Find("{'Age':{$lt:25}}").Limit(1).FirstOrDefault();
            Assert.IsNotNull(lt1);
            var lt2 = MongoCollection.Find(c => c.Age < 25).Limit(1).FirstOrDefault();
            Assert.IsNotNull(lt2);

            //Age >= 40
            var gte1 = MongoCollection.Find("{'Age':{$gte:40}}").Limit(1).FirstOrDefault();
            Assert.IsNotNull(gte1);
            var gte2 = MongoCollection.Find(c => c.Age >= 49).Limit(1).FirstOrDefault();
            Assert.IsNotNull(gte2);

            //Age <= 60
            var lte1 = MongoCollection.Find("{'Age':{$lte:60}}").Limit(1).FirstOrDefault();
            Assert.IsNotNull(lte1);
            var lte2 = MongoCollection.Find(c => c.Age <= 60).Limit(1).FirstOrDefault();
            Assert.IsNotNull(lte2);

            //Age >= 18 <= 60
            var complex1 = MongoCollection.Find("{'Age':{$gte:18,$lte:60}}").Limit(1).FirstOrDefault();
            Assert.IsNotNull(complex1);
            var complex2 = MongoCollection.Find(c => c.Age >= 18 && c.Age <= 60).Limit(1).FirstOrDefault();
            Assert.IsNotNull(complex2);

            //$all
            var all1 = MongoCollection.Find("{'Age':{$all:[57]}}").Count();
            Assert.IsNotNull(all1 > 0);
        }

        [TestMethod]
        public void FindByLinQTest()
        {
            //var customer = MongoCollection.Find<Customer>();
        }

        [TestCleanup()]
        public void TestCleanup()
        {
        }
    }
}