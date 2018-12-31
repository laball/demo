using System;
using Autofac.Extras.DynamicProxy;
using Core.Integrated;

namespace Core.Service
{
    /// <summary>
    ///  设置拦截器
    /// </summary>
    [Intercept(typeof(DemoInterceptor))]
    public class DemoService : IDemoService
    {
        private static readonly Random _rd = new Random();

        public string GetString()
        {
            return _rd.Next(1000, 9999).ToString(); 
        }

        public int GetValue()
        {
            return _rd.Next(100, 999);
        }
    }
}