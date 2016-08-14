using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopAlliance.Intercept;
using Spring.Aop.Framework;
using Spring.Caching;

namespace AOPDemo
{
    public interface ICommand
    {
        object Execute(object context);
    }

    public class ServiceCommand : ICommand
    {
        [CacheResult]
        public object Execute(object context)
        {
            Console.Out.WriteLine("Service implementation : [{0}]", context);
            return null;
        }
    }

    public class ConsoleLoggingAroundAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            Console.Out.WriteLine("Advice executing; calling the advised method...");
            object returnValue = invocation.Proceed();
            Console.Out.WriteLine("Advice executed; advised method returned " + returnValue);
            return returnValue;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            ProxyFactory factory = new ProxyFactory(new ServiceCommand());
            factory.AddAdvice(new ConsoleLoggingAroundAdvice());
            ICommand command = (ICommand)factory.GetProxy();
            command.Execute("This is the argument");
        }
    }
}