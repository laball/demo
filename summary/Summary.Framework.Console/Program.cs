using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

        static async System.Threading.Tasks.Task Main(string[] args)
        //static void Main(string[] args)
        {
            // question-1
            //System.Console.WriteLine($"Entry Main ThreadId {Thread.CurrentThread.ManagedThreadId}");

            //var thread = new Thread(Foo);
            //thread.Start();

            //System.Threading.Thread.Sleep(1000);

            //thread.Abort();

            //System.Console.WriteLine($"After Abort ThreadId {Thread.CurrentThread.ManagedThreadId}");

            //question-2
            //Foo foo = (IFoo)null;
            //foo.Name = "lindexi";

            //question-3
            //Foo foo = await(object)null;
            //foo.Name = "lindexi";


            //var strs = File.ReadAllLines("111.txt");
            //var deserialize_keywords = string.Join(",", strs.Select(c => $"'{c}'"));


            //var url = "translate.googleapis.com/translate_static/css/translateelement.css";
            //var domain = "translate.googleapis.com";

            //var ddd = url.IndexOf(domain, StringComparison.OrdinalIgnoreCase);

            //if (url.IndexOf(domain, StringComparison.OrdinalIgnoreCase) > 0)
            //{

            //}

            //var result1 = AsyncContext.Run(() => new AnyTypeCanBeAwait().Test(12));
            //System.Console.WriteLine(result1);

            //var result2 = AsyncContext.Run(() => AnyTypeCanBeAwait.Test0(1.2));
            //System.Console.WriteLine(result2);

            //new AsyncVoid().Test();
            //new AsyncTask().Test();

            //bool complete = false;
            //var t = new Thread(() =>
            //{
            //    bool toggle = false;
            //    while (!complete)
            //    {
            //        //Thread.MemoryBarrier();
            //        toggle = !toggle;
            //    }
            //});
            //t.Start();
            //Thread.Sleep(1000);
            //complete = true;
            //t.Join();        // 无限阻塞

            //Volatile

            //Thread.VolatileRead

            //Interlocked.MemoryBarrier

            //for (int i = 1; i < 6; i++)
            //{
            //    var tem = i;//没有这个多此一举的变量，差距很大
            //    System.Threading.Tasks.Task.Factory.StartNew(() =>
            //    {
            //        System.Console.WriteLine($"{tem}");
            //    });
            //}

            //AsyncTaskMethodBuilder<int> dd;
            //TaskAwaiter ff;
            //ConfiguredTaskAwaitable dd1;
            //System.Console.WriteLine($"end");
            System.Console.ReadLine();
        }


        private static void Foo()
        {
            try
            {
                while (true)
                {
                    System.Console.WriteLine($"Foo ThreadId {Thread.CurrentThread.ManagedThreadId}");
                }
            }
            finally
            {
                System.Console.WriteLine("尝试调用 Foo 函数执行这一句代码");
            }
        }

    }


    class IFoo
    {

    }

    class Foo
    {

        public string Name { get; set; }

        public static implicit operator Foo(IFoo b) => new Foo();

        //public static implicit operator Foo(object b) => new Foo();
    }

    static class ObjectWaitableExtensions
    {
        public static ObjectWaiter GetAwaiter(this object obj)
        {
            return new ObjectWaiter();
        }
    }


    class ObjectWaiter : INotifyCompletion
    {
        public bool IsCompleted { get; private set; }

        public object GetResult()
        {
            return new object();
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }
    }








}