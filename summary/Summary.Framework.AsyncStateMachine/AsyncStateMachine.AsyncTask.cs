using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if !DEBUG
using System.Runtime.InteropServices;
#endif
using System.Threading;

namespace Summary.Framework.AsyncStateMachine
{
    public class AsyncTask
    {
        //public async System.Threading.Tasks.Task Test()
        //{
        //    await System.Threading.Tasks.Task.Factory.StartNew(() =>
        //    {
        //        Thread.Sleep(1000);
        //    });
        //}

        //// Methods
        //[AsyncStateMachine(typeof(<Test>d__0))]
        //public Task Test()
        //{
        //    <Test>d__0 d__;
        //    d__.<>t__builder = AsyncTaskMethodBuilder.Create();
        //    d__.<>1__state = -1;
        //    d__.<>t__builder.Start<<Test>d__0>(ref d__);
        //    return d__.<>t__builder.Task;
        //}

        //// Nested Types
        //[Serializable, CompilerGenerated]
        //private sealed class <>c
        //{
        //    // Fields
        //    public static readonly AwaitTask.<>c <>9 = new AwaitTask.<>c();
        //    public static Action <>9__0_0;
        //
        //    // Methods
        //    internal void <Test>b__0_0()
        //    {
        //        Thread.Sleep(0x3e8);
        //    }
        //}

        // 以下是手动还原的状态机代码

        /// <summary>
        /// 执行顺序：
        /// ThreadId 1 before stateMachine._builder.Start
        /// ThreadId 1 MoveNext _state -1
        /// ThreadId 1 !awaiter.IsCompleted
        /// ThreadId 1 after stateMachine._builder.Start
        /// ThreadId 3 Thread.Sleep
        /// ThreadId 3 MoveNext _state 0
        /// ThreadId 3 this._state = num = -1
        /// ThreadId 3 awaiter.GetResult()
        /// ThreadId 3 this._builder.SetResult()
        /// 
        /// 初始化时，状态机 (_state=-1)；
        /// 在第一次执行 MoveNext 中的 AwaitUnsafeOnCompleted 后即返回Task (_state = 0)；
        /// 在异步 Action(根据上下文控制可能不一定是另外一个线程)执行完成后，第二次执行 MoveNext (_state=-2)
        /// </summary>
        public void Test()
        {
            var stateMachine = new AsyncVoidStateMachine
            {
                _builder = AsyncTaskMethodBuilder.Create(),
                _this = this,
                _state = -1
            };

            Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} before stateMachine._builder.Start");
            stateMachine._builder.Start(ref stateMachine);
            Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} after stateMachine._builder.Start");
        }

        [Serializable, CompilerGenerated]
        private sealed class class_c//<>c
        {
            // Fields
            public static readonly AsyncTask.class_c c = new AsyncTask.class_c();//.<>c<>9 = new AsyncVoid.<>c();
            public static Action action;//<>9__0_0;

            // Methods
            internal void fun()//<Test>b__0_0()
            {
                Thread.Sleep(0x3e8);
                Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} Thread.Sleep");
            }
        }

#if DEBUG
        /// <summary>
        /// Debug 模式使用的是 class
        /// Release 模式使用的是 struct
        /// 
        /// 1-编译时优化；
        /// 2-当 awaitable 已经完成时，可以不分配内存；
        /// 3-Debug 模式下编译时优化没有必要，并且会增加VS调试特性的实现难度；
        /// 
        /// Making the state-machine a struct is a compiler optimization. 
        /// It enables not allocating memory when the awaitable is already completed when awaited. 
        /// That optimization isn't necessary while debugging and it makes implementing debugging features in Visual Studio harder.
        /// 
        /// from https://stackoverflow.com/questions/32548509/what-is-the-purpose-of-iasyncstatemachine-setstatemachine/32548975?r=SearchResults#32548975
        /// </summary>
        [CompilerGenerated]
        private sealed class AsyncVoidStateMachine : IAsyncStateMachine
#else
        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct AsyncVoidStateMachine : IAsyncStateMachine
#endif
        {
            // Fields
            public int _state;
            public AsyncTask _this;
            public AsyncTaskMethodBuilder _builder;
            private TaskAwaiter _awaiter;

            // TODO: 这里可能会产生一些闭包的字段

            /// <summary>
            /// 状态机的核心方法
            /// 会执行两次或以上
            /// </summary>
            public void MoveNext()
            {
                Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} MoveNext _state {_state}");

                int num = this._state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        //******************************************************************************************************************************************************************
                        //这部分代码，跟实际执行的代码相关
                        //awaiter = System.Threading.Tasks.Task.CompletedTask.GetAwaiter();
                        awaiter = System.Threading.Tasks.Task.Factory.StartNew(AsyncTask.class_c.action ?? (AsyncTask.class_c.action = new Action(AsyncTask.class_c.c.fun))).GetAwaiter();
                        //******************************************************************************************************************************************************************
                        if (!awaiter.IsCompleted)
                        {
                            Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} !awaiter.IsCompleted");
                            this._state = num = 0;
                            this._awaiter = awaiter;
                            var stateMachine = this;
                            this._builder.AwaitUnsafeOnCompleted<TaskAwaiter, AsyncVoidStateMachine>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this._awaiter;
                        this._awaiter = new TaskAwaiter();
                        this._state = num = -1;
                        Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} this._state = num = -1");
                    }

                    awaiter.GetResult();
                    Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} awaiter.GetResult()");
                }
                catch (Exception exception)
                {
                    this._state = -2;
                    this._builder.SetException(exception);
                    return;
                }
                this._state = -2;
                this._builder.SetResult();
                Trace.WriteLine($"ThreadId {Thread.CurrentThread.ManagedThreadId} this._builder.SetResult()");
            }

            /// <summary>
            /// If you look at the same code compiled in release the state-machine will indeed be a struct 
            /// and the SetStateMachine method will not be empty as this is the method that 
            /// moves the state-machine from the stack to the heap:
            /// 
            /// 最后一句不是很理解；
            /// </summary>
            /// <param name="stateMachine"></param>
            [DebuggerHidden]
            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
#if DEBUG
                // Debug 模式时，此函数是空的；
#else
                this._builder.SetStateMachine(stateMachine);
#endif
            }
        }
    }
}