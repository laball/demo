using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Threading;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                var ss1 = new MyClass();
                var ss2 = new MyClass();

                using (var ss3 = new MyClass())
                {

                }
            });

            Console.WriteLine("sleep 1s ");

            Thread.Sleep(1000);

            GC.Collect();

            Console.ReadLine();
        }
    }

    public class MyClass : IDisposable
    {
        static MyClass()
        {
            Console.WriteLine("static constructor");
        }

        public MyClass()
        {
            Console.WriteLine("default constructor");
        }

        ~MyClass()
        {
            Dispose(false);
            Console.WriteLine("~ constructor");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("disposing:true");

                    // Release managed resources
                }

                // Release unmanaged resources

                m_disposed = true;
            }
        }

        private bool m_disposed;
    }

}