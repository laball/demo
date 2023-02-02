using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Summary.Framework.Test
{
    [TestClass]
    public class ConcurrentDictionaryTest
    {
        [TestMethod]
        public void Test1()
        {
            var count = 0;
            while (count < 10000)
            {
                IDictionary<string, string> dic = new ConcurrentDictionary<string, string>();

                var data = Enumerable.Range(1, 1000);

                Parallel.ForEach(data, jobId =>
                {
                    dic.Add(jobId.ToString(), "1");
                });

                Assert.IsTrue(dic.Keys.Any(c => c == null));
            }
        }

    }
}