﻿using System;
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

    internal class Program
    {
        private static void Main(string[] args)
        {
            var ddd = HttpUtility.UrlEncode("河南省直三院", Encoding.UTF8).ToUpper();

            var date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59");
            Console.ReadLine();

            GC.Collect();
        }

        private static void ReadFileTest()
        {
            StreamReader reader = new StreamReader("CCS_20161228.log");
            StreamWriter writer = new StreamWriter("111.txt");

            var sql = "INSERT INTO S_MessagePush (Type,Cost) VALUES('{0}',{1})";
            var line = reader.ReadLine();
            var count = 0;
            while (line != null)
            {
                var fields = line.Split(' ');
                if (fields != null && fields.Length >= 2)
                {
                    writer.WriteLine(string.Format(sql, fields[0], fields[1]));
                }

                line = reader.ReadLine();
                count++;
            }

            writer.Close();
            reader.Close();

            Console.WriteLine("end " + count);
        }
    }
}