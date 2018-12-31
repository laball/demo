using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tt = Test();


            //int.TryParse

            var t1 = DateTime.Parse("2018-06-11 17:00:00");
            var t2 = DateTime.Parse("2018-06-12 00:00:00");

            var hours = t2.Subtract(t1).TotalHours;

            //var time = DateTime.Parse("2018-06-14 18:03:19").ToString("yyyy-MM-dd HH:mm:ss");



            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var config = configuration["ConnectionStrings:Abp.Redis.Cache"];



            //var reg = new Regex("^[a-zA-Z0-9_-]{1,9}");
            var reg = new Regex("^[a-zA-Z0-9_-]{1,9}");//字母，数字，短横线（中，下）
            var test = "_";
            var re = reg.IsMatch(test);

            int interval = 16;
            string str = null;
            int.TryParse(str, out interval);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static int Test()
        {
            int result = 0;
            try
            {
                result = 1;
                return result;
            }
            catch (Exception)
            {

                return -1;
            }
            finally
            {
                result = 2;
                System.Diagnostics.Trace.WriteLine("125");
            }
        }

    }


    public static class Extentions
    {
        public static int WeekOfYear(this DateTime date)
        {
            return (int)Math.Ceiling(date.DayOfYear * 1.0d / 7);
        }
    }

    public class MyClass
    {

        public MyClass(int a)
        {

        }

    }


    public abstract class MyClassBase : MyClass
    {

        public MyClassBase():base(0)
        {

        }

        public MyClassBase(short b):base(b)
        {

        }




        protected MyClassBase(int a) : base(a)
        {

        }

    }




}
