using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summary.Framework.Task
{
    public class AnyTypeCanBeAwait
    {
        // <summary>
        // 可以 await 的类型，需要满足的条件：
        // 1- 支持 GetAwaiter 方法；一般使用扩展方法即可；
        // 2- GetAwaiter 返回的对象支持 
        //    2-1 GetResult()
        //    2-2 bool IsCompleted { get; }
        //    2-3 实现 INotifyCompletion 接口
        // </summary>
        // <param name="n"></param>
        // <returns></returns>
        // public static async Task<T> Test0<T>(T n) => await n;
        //public static async Task<int> Test0(int n) => await n;

        //public static async void Test1() => await System.Threading.Tasks.Task.CompletedTask;

        //public static async System.Threading.Tasks.Task Test2() => await System.Threading.Tasks.Task.CompletedTask;
    }
}