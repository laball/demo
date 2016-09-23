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
            var rd = new Random();

            for (int i = 0; i < 100; i++)
            {
                Trace.WriteLine(rd.Next(100000, 999999));
            }

            Console.ReadLine();
        }
    }


    public class RquestEventArgs : EventArgs
    {
        public int ID { get; set; }

        public string Message { get; set; }
    }


    public class MyClass
    {
        public static EventHandler<RquestEventArgs> OnRequest { get; set; }

        private void FireRequest(RquestEventArgs args)
        {
            if (OnRequest != null)
            {
                OnRequest(this, args);
            }
        }



        public void UpdateUserInfo(int userID, string userName)
        {
            var success = false;
            success = true;

            int id = 10;

            var args = new RquestEventArgs
            {
                ID = id,
                Message = userName
            };

            FireRequest(args);
        }


    }

}