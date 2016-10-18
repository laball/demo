using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    public class Dog : IRun
    {
        public void Run()
        {
            Console.WriteLine("Run...");
        }
    }
}