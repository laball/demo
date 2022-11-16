using Nito.AsyncEx;

namespace Summary.Framework.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = AsyncContext.Run(() => AnyTypeCanBeAwait.Test(12));

            System.Console.WriteLine(result);
            System.Console.WriteLine("end");
            System.Console.ReadLine();
        }
    }
}
