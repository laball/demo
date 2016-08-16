using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Demo
{
    class Program
    {
        private static void Main(string[] args)
        {
            var patterm = @"1\d{10}$";

            var  phones = new []{"15900860546", "159008605461", "15900860546q","1590086054*", "0371-65636337", "2044563", "" };

            var realPhones = from c in phones
                             where Regex.IsMatch(c, patterm)
                             select c;

            Trace.WriteLine(string.Join("\r\n", realPhones));


            Console.ReadLine();
        }
    }
}