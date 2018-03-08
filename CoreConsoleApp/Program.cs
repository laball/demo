using System;
using System.Linq;

namespace CoreConsoleApp
{
    /// <summary>
    /// <seealso cref="System.Diagnostics.Debug.Write(object)"/>
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new
            {
                Name = "Laball Lee",
                Code = "19870107"
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            System.Console.WriteLine(json);

            System.Console.Write("eeee");
            System.Console.ReadLine();
        }
    }
}