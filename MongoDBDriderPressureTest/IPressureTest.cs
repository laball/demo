using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    //BuildVision
    //https://visualstudiogallery.msdn.microsoft.com/23d3c821-ca2d-4e1a-a005-4f70f12f77ba
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
