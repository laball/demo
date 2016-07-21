using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    public class PressureTestBase : IPressureTest
    {
        static readonly Random rd = new Random();

        public long InsertCount { get; set; }
        public int ThreadCount { get; set; }
        public InsertMode Mode { get; set; }
        public int BulkSize { get; set; }
        public IMongoCollection<Customer> MongoCollection { get; set; }

        public virtual void Run()
        {
            
        }

        protected void BulkWrite(long count)
        {
            var cache = new WriteModel<Customer>[BulkSize];
            var options = new BulkWriteOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                cache[i % BulkSize] = new InsertOneModel<Customer>(customer);
                if (((i + 1) % BulkSize) == 0)
                {
                    MongoCollection.BulkWrite(cache, options);
                }
            }
        }

        protected void BulkWriteAsync(long count)
        {
            var cache = new WriteModel<Customer>[BulkSize];
            var options = new BulkWriteOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                cache[i % BulkSize] = new InsertOneModel<Customer>(customer);
                if (((i + 1) % BulkSize) == 0)
                {
                    MongoCollection.BulkWriteAsync(cache, options);
                }
            }
        }

        protected void InsertMany(long count)
        {
            var cache = new Customer[BulkSize];
            var options = new InsertManyOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                cache[i % BulkSize] = customer;
                if (((i + 1) % BulkSize) == 0)
                {
                    MongoCollection.InsertMany(cache, options);
                }
            }
        }

        protected void InsertManyAsync(long count)
        {
            var cache = new Customer[BulkSize];
            var options = new InsertManyOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                cache[i % BulkSize] = customer;
                if (((i + 1) % BulkSize) == 0)
                {
                    MongoCollection.InsertManyAsync(cache, options);
                }
            }
        }

        protected void InsertOne(long count)
        {
            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                MongoCollection.InsertOne(customer);
            }
        }

        protected void InsertOneAsync(long count)
        {
            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                MongoCollection.InsertOneAsync(customer);
            }
        }

        protected void InsertOneAsyncWithOption(long count)
        {
            var options = new InsertOneOptions
            {
                BypassDocumentValidation = true
            };

            for (int i = 0; i < count; i++)
            {
                Customer customer = BuildCustomer(i);
                MongoCollection.InsertOneAsync(customer, options);
            }
        }

        protected static Customer BuildCustomer(int i)
        {
            return new Customer
            {
                Name = "Test_" + i.ToString("00000000"),
                Info = new CustomerInfo
                {
                    Phone = "1590086" + rd.Next(1000, 9999).ToString(),
                    Address = "上海市南京西路1256号"
                },
                Orders = new List<Order>(new Order[] { new Order { ID = 11, Name = "order11", BuyTime = DateTime.Now }, new Order { ID = 12, Name = "order12", BuyTime = DateTime.Now } })
            };
        }
    }
}
