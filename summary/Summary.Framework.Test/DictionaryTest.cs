using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Summary.Framework.Test
{
    [TestClass]
    public class DictionaryTest
    {
        /// <summary>
        /// 并发向 Dictionary 写入数据时，可能会导致字典中的Key是null的情况；
        /// 非稳定复现
        /// </summary>
        [TestMethod]
        public void Appear_Key_Is_Null_In_Multy_Thread()
        {
            var dic = new Dictionary<string, string>();
            //var dic = new Dictionary<string, string>(1000+1);// 设置初始值只能降低概率，不能完全避免；

            var data = Enumerable.Range(1, 1000);

            Parallel.ForEach(data, jobId =>
            {
                dic.Add(jobId.ToString(), "1");
            });

            Assert.IsTrue(dic.Keys.Any(c => c == null));
        }
    }
}
