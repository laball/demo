using System;
using System.Threading;

namespace GC.Demo.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var person = new Person() { Id = 3, Name = "Person1" }) ;

            Thread.Sleep(2000);

            System.GC.Collect();

            Thread.Sleep(2000);

            System.GC.Collect();

            System.Console.ReadLine();
        }
    }


    public class Person : IDisposable
    {
        public static int Type = 10;
        public int Id { get; set; }
        public string Name { get; set; }

        public Person()
        {
            System.Diagnostics.Trace.WriteLine("Person.ctor");
        }

        ~Person()
        {
            System.Diagnostics.Trace.WriteLine("Person.~ctor");
        }

        protected void Dispose(bool dispose)
        {
            if (dispose)
            {
                System.Diagnostics.Trace.WriteLine("释放非托管资源");

                //System.GC.SuppressFinalize(this);//
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("释放托管资源");
            }
        }

        public void Close()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }








}