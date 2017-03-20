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

    enum Color : byte
    {
        Red,
        Green,
        Blue,
        Orange
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            //char a = 'a';
            //char z = 'z';

            //char[] chArray = new char[10000];
            //Random rd = new Random();

            //for (int i = 0; i < 10000; i++)
            //{
            //    chArray[i] = (char)rd.Next((int)(a),(int)(z));
            //}

            //var str = new string(chArray);



            int i = 5;
            int j = 5;
            if (Object.ReferenceEquals(i, j))
                Console.WriteLine("Equal");
            else
                Console.WriteLine("Not Equal");


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