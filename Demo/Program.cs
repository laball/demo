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
<<<<<<< HEAD
            var ddd = "1111111[CDATA[url?org={org}&mobile={mobile}&task={task}]]2222222";
            var ppp = @"\[CDAeTA\[(.*?)\]\]";
=======
            var lNum = 120L;
            var fNum = 3.141592653f;
            var dNum = 3.141592653D;
            var bNum = 0x001;

            Trace.WriteLine(lNum);
            Trace.WriteLine(fNum);
            Trace.WriteLine(dNum);
            Trace.WriteLine(bNum);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
>>>>>>> c9d16bd... 添加测试代码

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