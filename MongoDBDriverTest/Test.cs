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
        public void GetAllTest()
        {
            var count = MongoCollection.AsQueryable().Count();
        }

        [TestCleanup()]
        public void TestCleanup()
        {
        }

    }

}
