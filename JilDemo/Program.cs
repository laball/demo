using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jil;

namespace JilDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var times = new List<long>();

            for (int i = 0; i < 1000; i++)
            {
                var obj = new
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = new { ID = i, Code = "Code" },
                    Code = "Code",
                    Items = new[]
                    {
                        new  { ID = i, Name = "Name1" },
                        new { ID = i+1, Name = "Name1" },
                        new { ID = i+2, Name = "Name1" }
                    }
                };

                var watch = new Stopwatch();
                watch.Start();

                var json = JSON.Serialize(obj);

                watch.Stop();

                var time = watch.ElapsedMilliseconds;
                times.Add(time);
            }

            Trace.WriteLine(times.Sum() / (times.Count * 1.0));
        }
    }
}