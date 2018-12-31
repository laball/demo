using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var one = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var second = new[] { 7, 8, 9 };

            var toAdd= one.Except(second);
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


            var d = 0m;

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

        public static async Task SimpleBodyAsync()
        {

            //IAsyncStateMachine

            Console.WriteLine("Hello, Async World!");
        }

    }

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

}
