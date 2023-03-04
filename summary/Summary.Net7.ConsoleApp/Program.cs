using System.Runtime.CompilerServices;

namespace Summary.Net7.ConsoleApp
{
    internal class Program
    {
        //static void Main(string[] args)
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //question-3
            //Foo foo = await (object)null;
            //foo.Name = "lindexi";

            //var max = GC.MaxGeneration;

            var listold = new List<string> { "11", "22", "33", "44" };
            var listnew = new List<string> { "11", "22", "33", "44", "55" };

            var isAddNewItem = listnew.Any(c => !listold.Contains(c));


            var dic1 = new Dictionary<string, string>() { { "11", "aa" }, { "22", "bb" } };
            var dic2 = new Dictionary<string, string>() { { "11", "aa" }, { "22", "bbb" } };

            var isTitleChanged = dic2.Any(c => dic1.ContainsKey(c.Key) && dic1[c.Key] != c.Key);














            Console.WriteLine("Hello, World!");
            Console.ReadLine();
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

        public Foo GetResult()
        {
            return new Foo();
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }
    }

}