using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Shared;
using System.Collections.Generic;
using System.Linq;
using SamusMongoClientTest;

namespace MongoDBDriverTest
{
    [TestClass]
    public class Test
    {
        public const string ConnectionString = "mongodb://localhost";
        public const string DataBaseName = "TestDB";
        public const string CollectionName = "TestDB.Customer";
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase MongoDatabase { get; set; }
        public IMongoCollection<Customer> MongoCollection { get; set; }

        [TestInitialize()]
        public virtual void TestInitialize()
        {
            MongoClient = new MongoClient(ConnectionString);
            MongoDatabase = MongoClient.GetDatabase(DataBaseName);
            MongoCollection = MongoDatabase.GetCollection<Customer>(CollectionName);
        }

        [TestMethod]
        public void TestMethod1()
        {

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

        [TestCleanup()]
        public void TestCleanup()
        {
        }

    }

}
