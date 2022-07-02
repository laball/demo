using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {


        static void Main(string[] args)
        {
            new ConClass().test(100);

            StringBuilder sb;

            var rrr = "" == String.Empty;
            var rrr2 = "".Equals(String.Empty);

            var sb1 = new StringBuilder("123");
            var sb2 = new StringBuilder("123");

            var sb11 = sb1 == sb2;
            var sb22 = sb1.Equals(sb2);




            var values = new int[] { 1, 2, 5, 2, 3, 5, 5, 3, 4, 6, 3, 3 };

            var dd = values.GroupBy(c => c).OrderByDescending(c => c.Count()).FirstOrDefault().Key;





            Console.ReadLine();


            var one = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var second = new[] { 7, 8, 9 };

            var toAdd = one.Except(second);
            var toDelete = second.Except(one.Intersect(second));


            var items = Enumerable.Range(1, 10);
            var cords = //(
                from c in items
                select new MyClass
                {
                    CoordX = c,
                    CoordY = c
                };//).ToList();

            var type = cords.GetType();

            foreach (var cord in cords)
            {
                cord.CoordY += cord.CoordY;
            }


            //var d = 0m;

            var type1 = typeof(List<string>);
            var type2 = typeof(List<int>);

            if (type1 == type2)
            {
                Console.WriteLine("==");
            }
            else
            {
                Console.WriteLine("!=");
            }


            decimal count = 0;

            if (count == 0)
            {
                Console.WriteLine("11");
            }


            Console.ReadLine();
        }




        public static int GetNum()
        {
            int Num = 1;
            try
            {
                Console.WriteLine("try");
                return GetNum2(Num);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ++Num;
                Console.WriteLine("finally");
            }
        }


        public static void GetByRef(ref int num)
        {

        }


        public static void GetByOut(out int num)
        {
            num = 1;

            return;
        }

        public static int GetNum2(int num)
        {
            Console.WriteLine("GetNum2");
            return num++;
        }


        public static async Task SimpleBodyAsync()
        {

            //IAsyncStateMachine

            Console.WriteLine("Hello, Async World!");
        }

    }



    public delegate void Do(int i);

    public class ClassInitTest
    {
        public int a = 10;

        public int b = 20;

        public static int c;

        public event Do @do;

        public event Action<int> ddd;

        public event Func<int, int> add;

        static ClassInitTest()
        {

            c = 30;

            Trace.WriteLine("static ctor");
        }

        public ClassInitTest()
        {
            Trace.WriteLine("ctor");
        }


    }


    public class ConClass
    {
        public void Do()
        {

        }

        public void test(int i)
        {
            lock (this)
            {
                if (i > 10)
                {
                    i--;
                    test(i);
                }
            }
        }
    }


    public abstract class AbstractClass : ConClass
    {
        public abstract void DoSomeThing();
    }


    public class MyClass<T> where T : new()
    {


    }


    public class Foo
    {
        static readonly int a;


        delegate void Do(int a);

        event Do edo;


        public Foo()
        {
            var a = 10;
            System.Diagnostics.Trace.WriteLine("Foo()");

            System.Diagnostics.Trace.WriteLine($"{typeof(Do).FullName}  {typeof(Do).BaseType.FullName}");

            //Do.Combine();

            edo += Foo_edo;
            edo -= Foo_edo;


        }

        private void Foo_edo(int a)
        {
            throw new NotImplementedException();
        }

        ~Foo()
        {
            System.Diagnostics.Trace.WriteLine("~Foo()");
        }

        public void Test()
        {

        }
    }


    [Serializable]
    public class MyClass
    {

        /// <summary>
        /// X坐标
        /// </summary>
        public double CoordX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double CoordY { get; set; }
    }


    public class Singleton
    {
        private static Singleton uniqueInstance;
        private static readonly object locker = new object();
        private Singleton()
        {
        }
        public static Singleton GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Singleton();
                    }
                }
            }
            return uniqueInstance;
        }
    }

}
