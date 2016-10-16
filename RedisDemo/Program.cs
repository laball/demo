using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private static RedisClient redisClient = new RedisClient("192.168.1.15", 6379);

        private static void Main(string[] args)
        {
            var keys = redisClient.GetAllKeys();

            foreach (var key in keys)
            {
                Trace.WriteLine(string.Format("Key:{0}", key));
            }

            var value = redisClient.Get<string>("name");

            redisClient.Add("expiresKey", "125", new TimeSpan(0, 0, 2));

            var data = new ComplexData
            {
                Names = Enumerable.Range(10, 50).Select(c => c.ToString()).ToList(),
                Nums = Enumerable.Range(100, 200).ToList(),
                fNums = Enumerable.Range(500, 700).Select(c => float.Parse(c.ToString())).ToList(),
                dNums = Enumerable.Range(500, 700).Select(c => double.Parse(c.ToString())).ToList()
            };

            var min = DateTime.MinValue;
            var max = DateTime.MaxValue;

            redisClient.Add("ComplexData", data);

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);

                if (redisClient.ContainsKey("expiresKey"))
                {
                    var vl = redisClient.Get<string>("expiresKey");
                    Trace.WriteLine(vl);
                }
                else
                {
                    Trace.WriteLine("no expiresKey Key in redis");
                }
            });

            var allKeys = redisClient.GetAllKeys();

            Console.ReadLine();
        }
    }
}