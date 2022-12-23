using Microsoft.VisualStudio.TestTools.UnitTesting;
using Summary.Framework.Task;

namespace Summary.Framework.Test
{
    [TestClass]
    public class AwaitTaskTest
    {
        [TestMethod]
        public async void TestTest()
        {
            await new AwaitTask().Test();
        }
    }
}