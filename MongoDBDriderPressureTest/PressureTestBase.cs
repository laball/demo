using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MongoDBDriderPressureTest
{
    public class PressureTestBase<T> : IPressureTest<T>
    {
        public long InsertCount { get; set; }
        public int BulkSize { get; set; }
        public InsertMode Mode { get; set; }
        public IMongoCollection<T> MongoCollection { get; set; }
        public int ThreadCount { get; set; }

        public virtual void Run(IEnumerable<T> elements)
        {
<<<<<<< .mine

=======

>>>>>>> .theirs
        }

        protected void BulkWrite(IEnumerable<T> elements, int bulkSize)
        {


			//see:http://stackoverflow.com/questions/8859533/adding-batch-upsert-to-mongodb


            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            if (bulkSize < 1)
            {
                throw new InvalidOperationException("bulkSize is small than 1.");
            }

            var cache = new WriteModel<T>[bulkSize];
<<<<<<< .mine
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            if (bulkSize < 1)
            {
                throw new InvalidOperationException("bulkSize is small than 1.");
            }

            var cache = new WriteModel<T>[bulkSize];
=======
            //see:http://stackoverflow.com/questions/8859533/adding-batch-upsert-to-mongodb
            var cache = new WriteModel<Customer>[BulkSize];









>>>>>>> .theirs
            var options = new BulkWriteOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            var index = 0;

            foreach (var item in elements)
            {
                cache[index % BulkSize] = new InsertOneModel<T>(item);

                if (((index + 1) % bulkSize) == 0)
                {
                    MongoCollection.BulkWrite(cache, options);
                }

                index++;
            }
        }

        protected void BulkWriteAsync(IEnumerable<T> elements, int bulkSize)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            if (bulkSize < 1)
            {
                throw new InvalidOperationException("bulkSize is small than 1.");
            }

            var cache = new WriteModel<T>[bulkSize];
            var options = new BulkWriteOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            var index = 0;
            foreach (var item in elements)
            {
                cache[index % BulkSize] = new InsertOneModel<T>(item);

                if (((index + 1) % bulkSize) == 0)
                {
                    MongoCollection.BulkWriteAsync(cache, options);
                }

                index++;
            }
        }

        protected void InsertMany(IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            var options = new InsertManyOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            MongoCollection.InsertMany(elements, options);
        }

        protected void InsertManyAsync(IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            var options = new InsertManyOptions
            {
                BypassDocumentValidation = true,
                IsOrdered = false
            };

            MongoCollection.InsertManyAsync(elements, options);
        }

        protected void InsertOne(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            MongoCollection.InsertOne(element);
        }

        protected void InsertOneAsync(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            MongoCollection.InsertOneAsync(element);
        }

        protected void InsertOneAsyncWithOption(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var options = new InsertOneOptions
            {
                BypassDocumentValidation = true
            };

            MongoCollection.InsertOneAsync(element, options);
        }
    }
}