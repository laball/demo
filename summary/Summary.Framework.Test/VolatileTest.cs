using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Summary.Framework.Test
{
    [TestClass]
    public class VolatileTest
    {
        private static volatile bool isExcuted1 = false;
        private static volatile bool isExcuted2 = false;

        [TestMethod]
        public void NotUseVolatileTest()
        {
            var switchTrue = false;

            Trace.WriteLine($"start");

            var t = new Thread(() =>
            {
                var x = 0;
                while (!switchTrue)
                {
                    x++;
                }

                isExcuted1 = true;
            });

            t.IsBackground = true;
            t.Start();

            Thread.Sleep(100);
            switchTrue = true;

            Thread.Sleep(1000);
#if DEBUG
            Assert.IsTrue(isExcuted1);// Debug时，没有优化，会执行
#else
            Assert.IsTrue(!isExcuted1);// Release时，已优化，不会执行
#endif
        }

        [TestMethod]
        public void UseVolatileTest()
        {
            var switchTrue = false;

            Trace.WriteLine($"start");

            var t = new Thread(() =>
            {
                var x = 0;
                while (!Volatile.Read(ref switchTrue))
                {
                    x++;
                }

                isExcuted2 = true;
            });

            t.IsBackground = true;
            t.Start();

            Thread.Sleep(100);
            switchTrue = true;

            Thread.Sleep(1000);
#if DEBUG
            Assert.IsTrue(isExcuted2);//使用 Volatile类方法 Debug 和 Release 都会执行；
#else
            Assert.IsTrue(isExcuted2);
#endif
        }
    }
}