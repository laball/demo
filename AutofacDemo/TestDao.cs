using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AutofacDemo
{
    public interface ITestDao
    {
        [Cache(Key = "Key_DefaultDept")]
        int GetDefaultDept();
    }

    public class TestDao : ITestDao
    {
        //[Cache(Key = "Key_DefaultDept")]
        public int GetDefaultDept()
        {
            Trace.WriteLine("GetDefaultDept return");
            return 4;
        }
    }
}
