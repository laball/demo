using Castle.DynamicProxy;
using System.Diagnostics;

namespace Core.Integrated
{
    /// <summary>
    /// 拦截器演示
    /// </summary>
    public class DemoInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Trace.WriteLine("before");
            invocation.Proceed();
            Trace.WriteLine("after");
        }
    }
}