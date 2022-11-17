using System.Runtime.CompilerServices;
using Nito.AsyncEx;

namespace Summary.Framework.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result1 = AsyncContext.Run(() => AnyTypeCanBeAwait.Test0(12));
            System.Console.WriteLine(result1);

            //var result2 = AsyncContext.Run(() => AnyTypeCanBeAwait.Test0(1.2));
            //System.Console.WriteLine(result2);

            System.Console.WriteLine("end");

            //AsyncTaskMethodBuilder<int> dd;

            ConfiguredTaskAwaitable dd1;

            System.Console.ReadLine();
        }
    }
}
