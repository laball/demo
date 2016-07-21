using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    interface IPressureTest
    {
        int ThreadCount { get; set; }
        long InsertCount { get; set; }
        InsertMode Mode { get; set; }
        /// <summary>
        /// 当且仅当Mode in {BulkWrite,BulkWriteAsync,InsertMany,InsertManyAsync}时有效
        /// </summary>
        int BulkSize { get; set; }
        IMongoCollection<Customer> MongoCollection { get; set; }
        void Run();
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
