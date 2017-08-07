using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace RedisDemo
{
    public class ComplexData
    {
        public IList<string> Names { get; set; }

        public IList<int> Nums { get; set; }

        public IList<float> fNums { get; set; }

        public IList<double> dNums { get; set; }
    }

    internal class Program
    {
        private static IRedisClient redis;// = new RedisClient("Haozhuo2015@10.140.234.217", 6379);

        public static string uri = "Haozhuo2015@10.140.234.217";
        public static PooledRedisClientManager pool;


        //ServiceStack.Redis从V4开始商业化，收到各种限制，一般使用3.9.71，V3的最后一个版本。
        private static void Main(string[] args)
        {
            var config = new RedisClientManagerConfig()
            {
                AutoStart = true,
                DefaultDb = 1,
                MaxWritePoolSize = 10,
                MaxReadPoolSize = 10,
            };

            pool = new PooledRedisClientManager(new string[] { uri }, new string[] { uri }, config);

            redis = pool.GetClient();


            SortedSetTest1();

            //SortedSetTest2();
            Console.WriteLine("end");
            Console.ReadLine();
        }

        public static void SortedSetTest1()
        {
            var keys = pool.GetClient().GetAllKeys();

            var str = "A,B,C,D,E,F,A,B,C,D,E,F,A,B,C,D,E,F,A,B,C,D,E,F";
            //var doctorIDs = new List<string>(str.Split(','));

            int count = 10;

            var doctorIDRepeatTime = 10000;
            var doctorIDs = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var repeatIds = new List<int>();
            for (int i = 0; i < doctorIDRepeatTime; i++)
            {
                repeatIds.AddRange(doctorIDs);
            }

            var ids = repeatIds.Select(c => c.ToString()).ToList();

            var watch = new Stopwatch();

            for (int i = 0; i < count; i++)
            {
                var key = i.ToString("");

                watch.Start();
                redis.AddRangeToList(key, ids);
                watch.Stop();
            }


            Console.WriteLine(string.Format("AddRangeToList cost:{0}", watch.ElapsedMilliseconds));

            watch = new Stopwatch();

            for (int i = 0; i < count; i++)
            {
                var key = "_" + i.ToString();

                watch.Start();
                redis.AddRangeToListEx(key, ids);
                watch.Stop();
            }

            Console.WriteLine(string.Format("AddRangeToListEx cost:{0}", watch.ElapsedMilliseconds));

            //创建机构可分配健管师ID缓存
            //var setid = "DoctorsToAssign_125";
            //redis.AddRangeToListEx(setid, ids);
            //redis.ExpireEntryIn(setid, TimeSpan.FromSeconds(30));

            //redis.Remove(setid);


            //从缓存中获取一个健管师ID
            //var dcotorId = redis.PopItemFromList(setid);

            //如果分配失败，则将健管师ID重新放入缓存中
            //redis.AddItemToList(setid, "F");

        }

        public static void SortedSetTest2()
        {
            var str = "A,B,C";
            var doctorIDs = new List<string>(str.Split(','));

            //创建机构可分配健管师ID缓存
            var setid = "DoctorsToAssign_4";
            redis.AddRangeToList(setid, doctorIDs);
            //redis.ExpireEntryIn(setid, TimeSpan.FromSeconds(30));


            //从缓存中获取一个健管师ID
            var dcotorId = redis.PopItemFromList(setid);

            //如果分配失败，则将健管师ID重新放入缓存中
            //redis.AddItemToList(setid, "F");

        }

        public static void Test1()
        {
            var keys = redis.GetAllKeys();

            foreach (var key in keys)
            {
                Trace.WriteLine(string.Format("Key:{0}", key));
            }

            var value = redis.Get<string>("name");

            redis.Add("expiresKey", "125", new TimeSpan(0, 0, 2));

            var data = new ComplexData
            {
                Names = Enumerable.Range(10, 50).Select(c => c.ToString()).ToList(),
                Nums = Enumerable.Range(100, 200).ToList(),
                fNums = Enumerable.Range(500, 700).Select(c => float.Parse(c.ToString())).ToList(),
                dNums = Enumerable.Range(500, 700).Select(c => double.Parse(c.ToString())).ToList()
            };

            var min = DateTime.MinValue;
            var max = DateTime.MaxValue;

            redis.Add("ComplexData", data);

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);

                if (redis.ContainsKey("expiresKey"))
                {
                    var vl = redis.Get<string>("expiresKey");
                    Trace.WriteLine(vl);
                }
                else
                {
                    Trace.WriteLine("no expiresKey Key in redis");
                }
            });

            var allKeys = redis.GetAllKeys();
        }
    }
}