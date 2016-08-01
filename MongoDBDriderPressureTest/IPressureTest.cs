using System.Collections.Generic;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    internal interface IPressureTest<T>
    {
        int ThreadCount { get; set; }
        long InsertCount { get; set; }
        int BulkSize { get; set; }
        InsertMode Mode { get; set; }
        IMongoCollection<T> MongoCollection { get; set; }

        void Run(IEnumerable<T> elements);
    }

    public enum InsertMode
    {
        BulkWrite,
        BulkWriteAsync,
        InsertMany,
        InsertManyAsync,
        InsertOne,
        InsertOneAsync,
        InsertOneAsyncWithOption
    }
}