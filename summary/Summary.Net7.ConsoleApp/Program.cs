namespace Summary.Net7.ConsoleApp
{
    internal class Program
    {
        private static volatile int x, y, a, b;

        private static int Count = 0;

        static void Main(string[] args)
        {
            while (true)
            {   
                var t1 = Task.Run(Test1);
                var t2 = Task.Run(Test2);
                Task.WaitAll(t1, t2);
                if (a == 0 && b == 0)
                {

                    //Volatile

                    //Thread

                    //MemoryBarrier

                    Interlocked.Increment(ref Count);

                    Console.WriteLine($"Count {Count} a={a} b={b}");
                }

                x = y = a = b = 0;

                if (Count >= 100)
                {
                    break;
                }

            }

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }

        static void Test1()
        {
            x = 1;
            //方案一，只用一个 Interlocked.MemoryBarrierProcessWide();test2不需要添加内存屏障。问题就可以解决
            //Interlocked.MemoryBarrierProcessWide();

            //方案二 Interlocked.MemoryBarrier(); 为什么不用这个内存屏障，即使添加了也还是会出现，必须同时在test、和test2中同时添加
            //Interlocked.MemoryBarrier();
            a = y;
        }

        static void Test2()
        {
            y = 1;

            //方案二 use Interlocked.MemoryBarrier();
            //Interlocked.MemoryBarrier();
            b = x;
        }

    }
}