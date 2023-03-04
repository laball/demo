using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Summary.Framework.Console
{
    internal class Program
    {
        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern void SetLastError(uint dwErrCode);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern void OutputDebugString(string lpOutputString);

        static async System.Threading.Tasks.Task Main(string[] args)
        //static void Main(string[] args)
        {

            MyClass mc = new MyClass();
            MyInterface1 mi1 = mc;
            MyInterface2 mi2 = mc;

            int i = MyClass.str.Length;
            uint j = MyClass.ui;

            mc.Method1();
            mi1.Method1();
            mi1.Method2();
            mi2.Method2();
            mi2.Method3();
            mc.Method3();


            //Test2();

            //var obj = new Foo();
            //IDisposable disposable = obj;

            //obj.Dispose();
            //disposable.Dispose();

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


        /// <summary>
        /// 在 debug 模式下不开启代码优化，所以需要用 release 模式下生成。
        /// 执行 dotnet build -c release --no-incremental 后运行代码，如果没有标记为易变，则不会打印 x。
        /// </summary>
        public static void Test2()
        {
            var switchTrue = false;

            var t = new Thread(() =>
            {
                var x = 0;
                //while (!switchTrue) // 如果没有标记变量为易变，编译器会把 while(!switchTrue) 优化为 while(true) 从而导致永远不会打印出 x 的值
                while (!Volatile.Read(ref switchTrue)) // 标记为易变，可以保证在调用时才进行取值，不会进行代码优化。
                {
                    x++;
                }
                System.Console.WriteLine($"x: {x}");
            });
            t.IsBackground = true;
            t.Start();

            Thread.Sleep(100);
            switchTrue = true;
            System.Console.WriteLine("ok");
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

    public interface MyInterface1
    {
        void Method1();
        void Method2();
    }
    public interface MyInterface2
    {
        void Method2();
        void Method3();
    }

    class MyClass : MyInterface1, MyInterface2
    {
        public static string str = "MyString";
        public static uint ui = 0xAAAAAAAA;
        public void Method1() { System.Console.WriteLine("Method1"); }
        public void Method2() { System.Console.WriteLine("Method2"); }
        public virtual void Method3() { System.Console.WriteLine("Method3"); }
    }

    class IFoo
    {

    }

    class Foo : IDisposable
    {

        public string Name { get; set; }

        void IDisposable.Dispose()
        {
            Trace.WriteLine("IDisposable.Dispose()");
        }

        public void Dispose()
        {
            Trace.WriteLine("Class.Dispose()");
        }

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