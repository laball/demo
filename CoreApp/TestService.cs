using System;
using System.Diagnostics;
using System.Threading;

namespace CoreApp
{
    public class TestService
    {
        public static void Run(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                Trace.WriteLine($"{DateTime.Now}: 1. 获取mysql");
                Thread.Sleep(2000);
                Trace.WriteLine($"{DateTime.Now}: 2. 获取redis");
                Thread.Sleep(2000);
                Trace.WriteLine($"{DateTime.Now}: 3. 更新monogdb");
                Thread.Sleep(2000);
                Trace.WriteLine($"{DateTime.Now}: 4. 通知kafka");
                Thread.Sleep(2000);
                Trace.WriteLine($"{DateTime.Now}: 5. 所有业务处理完毕");
                Thread.Sleep(2000);
            }
        }

    }
}
