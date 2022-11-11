// See https://aka.ms/new-console-template for more information
using System.Linq;

Console.WriteLine("Hello, World!");

//var values = new int[] { 1,2,5,2,3,5,5,3,4,6,3,3 };
//var ddd = values.GroupBy(c => c).OrderByDescending(c => c.Count()).FirstOrDefault()?.Key;
//Console.WriteLine(ddd);

var str = "纷纷为分为发我";
var len = str.Length;


Func<int, int> ddd = c => c + 1;

void Test()
{
    Thread.Sleep(100);
}

Console.ReadLine();
