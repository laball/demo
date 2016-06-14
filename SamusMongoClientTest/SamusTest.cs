using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamusMongoClientTest
{
    [TestClass]
    public class SamusTest
    {
        public const string ConnectionString = "mongodb://localhost";
        public const string DataBaseName = "TestDB";
        public const string CollectionName = "TestDB.Customer";

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
            var customer = new Customer
            {
                ID = 1,
                Name = "Test1",
                Info = new CustomerInfo
                {
                    Phone = "15900860546",
                    Address = "上海市南京西路1256号"
                },
                Orders = new List<Order>(new Order[] { new Order { ID = 11,Name = "order11",BuyTime = DateTime.Now },new Order { ID = 12,Name = "order12",BuyTime = DateTime.Now } })
            };

            MongoCollection.Insert(customer);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var items = MongoCollection.Linq().ToList();
        }
    }


    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CustomerInfo Info { get; set; }
        public IList<Order> Orders { get; set; }
    }

    public class CustomerInfo
    {
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BuyTime { get; set; }
    }




}
