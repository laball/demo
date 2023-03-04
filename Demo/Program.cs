using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Demo
{
    public class NullInstance<T> where T : class, new()
    {
        public static readonly T Instance;
    }

    //定义一个委托类型
    delegate void MyDelegate(string message);

    internal class Program
    {
        public static void Method1(string message)
        {
            Console.WriteLine("Method1 called: " + message);
        }
        public static void Method2(string message)
        {
            Console.WriteLine("Method2 called: " + message);
        }


        private static void Main(string[] args)
        {
            //创建多播委托
            MyDelegate delegate1 = new MyDelegate(Method1);
            MyDelegate delegate2 = new MyDelegate(Method2);
            MyDelegate delegate3 = delegate1 + delegate2;
            //调用多播委托
            delegate3("Hello World");

            //var ddd = HttpUtility.UrlEncode("河南省直三院", Encoding.UTF8).ToUpper();

            //var date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59");
            //Console.ReadLine();

            //GC.Collect();
        }

        //private static void ReadFileTest()
        //{
        //    StreamReader reader = new StreamReader("CCS_20161228.log");
        //    StreamWriter writer = new StreamWriter("111.txt");

        //    var sql = "INSERT INTO S_MessagePush (Type,Cost) VALUES('{0}',{1})";
        //    var line = reader.ReadLine();
        //    var count = 0;
        //    while (line != null)
        //    {
        //        var fields = line.Split(' ');
        //        if (fields != null && fields.Length >= 2)
        //        {
        //            writer.WriteLine(string.Format(sql, fields[0], fields[1]));
        //        }

        //        line = reader.ReadLine();
        //        count++;
        //    }

        //    writer.Close();
        //    reader.Close();

        //    Console.WriteLine("end " + count);
        //}
    }
}