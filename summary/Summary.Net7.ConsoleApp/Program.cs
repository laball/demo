using System.Runtime.CompilerServices;

namespace Summary.Net7.ConsoleApp
{
    internal class Program
    {
        //static void Main(string[] args)
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //question-3
            Foo foo = await (object)null;
            foo.Name = "lindexi";

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