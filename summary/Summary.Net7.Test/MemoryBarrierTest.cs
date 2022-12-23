namespace Summary.Net7.Test
{
    public class MemoryBarrierTest
    {
        private static volatile int x, y, a, b;

        /// <summary>
        /// from https://www.notion.so/lee1573/C-Volatile-5a61de5e0c684fb89b8077a66a1f2da3
        /// </summary>
        [Fact]
        public void BarrierTest()
        {
            while (true)
            {
                var t1 = Task.Run(Test1);
                var t2 = Task.Run(Test2);

                Task.WaitAll(t1, t2);

                if (a == 0 && b == 0)
                {
                    Assert.True(a == 0 && b == 0);
                    break;
                }

                x = y = a = b = 0;
            }
        }

        static void Test1()
        {
            x = 1;

            a = y;
        }

        static void Test2()
        {
            y = 1;

            b = x;
        }
    }
}