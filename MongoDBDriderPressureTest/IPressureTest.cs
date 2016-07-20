using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDriderPressureTest
{
    interface IPressureTest
    {
        int ThreadCount { get; set; }
        long InsertCount { get; set; }
        bool UsingBulk { get; set; }
        void Run();
    }
}
