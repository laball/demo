namespace Summary.Net7.Test
{
    public class MemoryBarrierTest
    {
        private static volatile int x, y, a, b;

        /// <summary>
        /// from https://www.notion.so/lee1573/C-Volatile-5a61de5e0c684fb89b8077a66a1f2da3
        /// 
        /// �������⣺
        /// a��b���ֶ�Ϊ0�����
        /// 
        /// </summary>
        [Fact]
        public void Memory_Barrier_Recurrence_Test()
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

        #region Solution-1

        /// <summary>
        /// �������1��������һ����� Interlocked.MemoryBarrierProcessWide(); ���
        /// �ŵ㣺ֻ��Ҫ��������һ�����ɣ��Ķ��ɱ���
        /// </summary>
        /// <param name="excuteTimes"></param>
        [Theory]
        [InlineData(10000)]
        public void Memory_Barrier_Solution_1_Test(int excuteTimes)
        {
            var times = 0;

            while (times < excuteTimes)
            {
                var t1 = Task.Run(Test_1_Solution_1);
                var t2 = Task.Run(Test_2_Solution_1);

                Task.WaitAll(t1, t2);

                Assert.False(a == 0 && b == 0);

                x = y = a = b = 0;
                times++;
            }
        }

        static void Test_1_Solution_1()
        {
            x = 1;

            Interlocked.MemoryBarrierProcessWide();

            a = y;
        }

        static void Test_2_Solution_1()
        {
            y = 1;

            b = x;
        }

        #endregion

        #region Solution-2

        /// <summary>
        ///  �������2��������ִ������� Interlocked.MemoryBarrier(); ���
        ///  ����ȱ�㣺�����©����һ���������ִ�в�����Ԥ�ڵ����
        /// </summary>
        /// <param name="excuteTimes"></param>
        [Theory]
        [InlineData(10000)]
        public void Memory_Barrier_Solution_2_Test(int excuteTimes)
        {
            var times = 0;

            while (times < excuteTimes)
            {
                var t1 = Task.Run(Test_1_Solution_2);
                var t2 = Task.Run(Test_2_Solution_2);

                Task.WaitAll(t1, t2);

                Assert.False(a == 0 && b == 0);

                x = y = a = b = 0;
                times++;
            }
        }

        static void Test_1_Solution_2()
        {
            x = 1;

            Interlocked.MemoryBarrier();

            a = y;
        }

        static void Test_2_Solution_2()
        {
            y = 1;

            Interlocked.MemoryBarrier();

            b = x;
        }

        #endregion
    }
}