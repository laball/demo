using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Summary.Framework.AsyncStateMachine;

namespace Summary.Framework.Test
{
    [TestClass]
    public class AsyncVoidTest
    {
        [TestMethod]
        public void TestTest()
        {
            new AsyncVoid().Test();
        }
    }
}
