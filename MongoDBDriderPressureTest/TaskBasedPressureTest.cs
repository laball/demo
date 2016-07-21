using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoEntity;

namespace MongoDBDriderPressureTest
{
    public class TaskBasedPressureTest : PressureTestBase
    {
        public override void Run()
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
                Task.Factory.StartNew(Work);
            }
        }

        public void Work()
        {
            var count = InsertCount / ThreadCount;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            switch (Mode)
            {
                case InsertMode.BulkWrite:
                    BulkWrite(count);
                    break;
                case InsertMode.BulkWriteAsync:
                    BulkWriteAsync(count);
                    break;
                case InsertMode.InsertMany:
                    InsertMany(count);
                    break;
                case InsertMode.InsertManyAsync:
                    InsertManyAsync(count);
                    break;
                case InsertMode.InsertOne:
                    InsertOne(count);
                    break;
                case InsertMode.InsertOneAsync:
                    InsertOneAsync(count);
                    break;
                case InsertMode.InsertOneAsyncWithOption:
                    InsertOneAsyncWithOption(count);
                    break;
                default:
                    break;
            }

            stopWatch.Stop();

            Trace.WriteLine(string.Format("{0} Cost:{1} ms", Mode.ToString(), stopWatch.ElapsedMilliseconds.ToString("0.000")));
        }

    }
}
