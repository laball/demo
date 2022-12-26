using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Summary.Framework.Console
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void SetLastError(uint dwErrCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void OutputDebugString(string lpOutputString);

        static void Main(string[] args)
        {
            //var result1 = AsyncContext.Run(() => new AnyTypeCanBeAwait().Test(12));
            //System.Console.WriteLine(result1);

            //var result2 = AsyncContext.Run(() => AnyTypeCanBeAwait.Test0(1.2));
            //System.Console.WriteLine(result2);

            //new AsyncVoid().Test();
            //new AsyncTask().Test();






            //Volatile

            //Thread.VolatileRead

            //Interlocked.MemoryBarrier

            for (int i = 1; i < 6; i++)
            {
                var tem = i;//没有这个多此一举的变量，差距很大
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    System.Console.WriteLine($"{tem}");
                });
            }

            //AsyncTaskMethodBuilder<int> dd;
            //TaskAwaiter ff;
            //ConfiguredTaskAwaitable dd1;
            System.Console.WriteLine($"end");
            System.Console.ReadLine();
        }
    }
}