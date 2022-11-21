using System.Runtime.CompilerServices;
using Nito.AsyncEx;
using Summary.Framework.AsyncStateMachine;

namespace Summary.Framework.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var result1 = AsyncContext.Run(() => new AnyTypeCanBeAwait().Test(12));
            //System.Console.WriteLine(result1);

            //var result2 = AsyncContext.Run(() => AnyTypeCanBeAwait.Test0(1.2));
            //System.Console.WriteLine(result2);

            //new AsyncVoid().Test();
            //new AsyncTask().Test();

            System.Console.WriteLine("end");

            //AsyncTaskMethodBuilder<int> dd;
            //TaskAwaiter ff;
            //ConfiguredTaskAwaitable dd1;

            System.Console.ReadLine();
        }
    }
}