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
            var ddd = "1111111[CDATA[url?org={org}&mobile={mobile}&task={task}]]2222222";

            //var ppp = @"\[CDATA\[(.*?)\]\]";
            var ppp = @"\[CDAeTA\[(.*?)\]\]";

            var ooo = Regex.Match(ddd,ppp);

            Trace.WriteLine(ooo.Groups[1].Value);

            var dddddd = Regex.Replace(ddd,ppp,"url?org=1&mobile=15900860546&task=123");

            string pattern = @"\b(\w+?)\s\1\b";
            string input = "This this is a nice day. What about this? This tastes good. I saw a a dog.";
            foreach(Match match in Regex.Matches(input,pattern,RegexOptions.IgnoreCase))
                Console.WriteLine("{0} (duplicates '{1}') at position {2}",
                                  match.Value,match.Groups[1].Value,match.Index);

            Console.ReadLine();
        }
    }
}