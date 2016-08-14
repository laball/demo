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
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ddd = "1111111[CDATA[url?org={org}&mobile={mobile}&task={task}]]2222222";
            var ppp = @"\[CDAeTA\[(.*?)\]\]";

            Trace.WriteLine(ooo.Groups[1].Value);

            Console.ReadLine();        }



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