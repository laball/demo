using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var date1 = new DateTime(2016, 10, 23, 0, 0, 0);
            var date2 = new DateTime(2016, 11, 22, 0, 0, 0);

            var days = date2.Subtract(date1).TotalDays;


        }
    }
}