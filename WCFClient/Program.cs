using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClient.DemoService;

namespace WCFClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new DemoServiceClient();
            var data = client.GetData(125);
            var composite = new CompositeType()
            {
                BoolValue = true
            };

            var type = client.GetDataUsingDataContract(composite);
        }
    }

    public class MyClass
    {
        public MyClass()
        {

        }
    }

}