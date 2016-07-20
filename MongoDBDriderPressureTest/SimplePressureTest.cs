using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDriderPressureTest
{
    public class SimplePressureTest : IPressureTest
    {
        public long InsertCount { get; set; }
        public int ThreadCount { get; set; }
        public bool UsingBulk { get; set; }
        public void Run()
        {

        }
    }
}
