using System;
using System.Linq;
using System.Collections.Generic;

namespace ElasticsearchDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var rd = new Random();

            var tt = "Test_" + rd.Next(1, 999999).ToString("000000");

            //Test.Test1();
            //Test.QueryTest1();

            var list = new List<int>()
            {
                1,2,3,4,5,6,
            };

            var dddd = list.ToArray().AsEnumerable();

            //list.RemoveRange(6 - 2, 2);
            list.Clear();




            var items = new[] { new MyClass { Name = "111" }, new MyClass { Name = "222" }, new MyClass { Name = "333" }, new MyClass { Name = "444" } }.ToList();
            Test(items);

            Console.ReadLine();
        }


        static void Test(List<MyClass> items)
        {
            var tempItems = items.Take(2).ToList();

            foreach (var item in tempItems)
            {
                items.Remove(item);
            }
        }


    }

    public class MyClass
    {
        public string Name { get; set; }
    }

}
