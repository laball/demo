using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBusDemo
{
    public class NullInstance<T> where T : class, new()
    {
        //public
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }
}