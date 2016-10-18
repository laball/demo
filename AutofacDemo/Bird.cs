using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    public class Bird : IFly
    {
        private IRun _run;

        public Bird(IRun run)
        {
            _run = run;
        }

        public void Fly()
        {
            Console.WriteLine("Fly...");

            _run.Run();
        }
    }
}