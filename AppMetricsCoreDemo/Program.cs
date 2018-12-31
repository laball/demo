using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppMetricsCoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var items = Enumerable.Range(1, 10);
            var cords = //(
                from c in items
                select new MyClass
                {
                    CoordX = c,
                    CoordY = c
                };//).ToList();

            var type = cords.GetType();

            foreach (var cord in cords)
            {
                cord.CoordY += cord.CoordY;
            }




            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }


    public class MyClass
    {

        /// <summary>
        /// X坐标
        /// </summary>
        public double CoordX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double CoordY { get; set; }
    }

}
