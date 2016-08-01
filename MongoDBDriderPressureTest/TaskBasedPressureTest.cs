using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    public class TaskBasedPressureTest : PressureTestBase<Customer>
    {
        private static readonly Random rd = new Random();

        public override void Run(IEnumerable<Customer> elements)
        {
            if (MongoCollection == null)
            {
                throw new InvalidOperationException("MongoCollection is null.");
            }

            if (InsertCount < 1)
            {
                throw new InvalidOperationException("Need InsertCount.");
            }

            if (ThreadCount < 1)
            {
                throw new InvalidOperationException("Need ThreadCount.");
            }

            for (int i = 0; i < ThreadCount; i++)
            {
                Task.Factory.StartNew(c => { Work(elements); }, elements);
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

        public void Work(IEnumerable<Customer> customers)
        {
            var count = InsertCount / ThreadCount;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            switch (Mode)
            {
                case InsertMode.BulkWrite:
                    BulkWrite(customers, BulkSize);
                    break;

                case InsertMode.BulkWriteAsync:
                    BulkWriteAsync(customers, BulkSize);
                    break;

                case InsertMode.InsertMany:
                    InsertMany(customers);
                    break;

                case InsertMode.InsertManyAsync:
                    InsertManyAsync(customers);
                    break;

                case InsertMode.InsertOne:

                    foreach (var item in customers)
                    {
                        InsertOne(item);
                    }
                    break;

                case InsertMode.InsertOneAsync:
                    foreach (var item in customers)
                    {
                        InsertOne(item);
                    }
                    break;

                case InsertMode.InsertOneAsyncWithOption:
                    foreach (var item in customers)
                    {
                        InsertOne(item);
                    }
                    break;

                default:
                    break;
            }

            stopWatch.Stop();

            Trace.WriteLine(string.Format("{0} Cost:{1} ms", Mode.ToString(), stopWatch.ElapsedMilliseconds.ToString("0.000")));
        }
    }
}