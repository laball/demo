﻿using System;
using System.Diagnostics;

namespace AOPDemo
{
    public static class AspectExtensions
    {
        [DebuggerStepThrough]
        public static AspectF Delay(this AspectF aspect, int milliseconds)
        {
            return aspect.Combine((work) =>
            {
                System.Threading.Thread.Sleep(milliseconds);
                work();
            });
        }

        [DebuggerStepThrough]
        public static void DoNothing()
        {
        }

        [DebuggerStepThrough]
        public static void DoNothing(params object[] whatever)
        {
        }

        public static AspectF MustBeNonDefault<T>(this AspectF aspect, params T[] args) where T : IComparable
        {
            return aspect.Combine((work) =>
            {
                T defaultvalue = default(T);
                for (int i = 0; i < args.Length; i++)
                {
                    T arg = args[i];
                    if (arg == null || arg.Equals(defaultvalue))
                    {
                        throw new ArgumentException(string.Format("Parameter at index {0} is null", i));
                    }
                }
                work();
            });
        }

        [DebuggerStepThrough]
        public static AspectF MustBeNonNull(this AspectF aspect, params object[] args)
        {
            return aspect.Combine((work) =>
            {
                for (int i = 0; i < args.Length; i++)
                {
                    object arg = args[i];
                    if (arg == null)
                    {
                        throw new ArgumentException(string.Format("Parameter at index {0} is null", i));
                    }
                }
                work();
            });
        }

        [DebuggerStepThrough]
        public static AspectF RunAsync(this AspectF aspect, Action completeCallback)
        {
            return aspect.Combine((work) => work.BeginInvoke(asyncresult =>
            {
                work.EndInvoke(asyncresult);
                completeCallback();
            }, null));
        }

        [DebuggerStepThrough]
        public static AspectF RunAsync(this AspectF aspect)
        {
            return aspect.Combine((work) => work.BeginInvoke(asyncresult =>
            {
                work.EndInvoke(asyncresult);
            }, null));
        }

        public static AspectF WhenTrue(this AspectF aspect, params Func<bool>[] conditions)
        {
            return aspect.Combine((work) =>
            {
                foreach (Func<bool> condition in conditions)
                {
                    if (!condition())
                    {
                        return;
                    }
                }
                work();
            });
        }
    }

    public class AspectF
    {
        internal Action<Action> Chain = null;

        internal Delegate WorkDelegate;

        public static AspectF Define
        {
            [DebuggerStepThrough]
            get
            {
                return new AspectF();
            }
        }

        public static void Retry(int retryDuration, int retryCount, Action<Exception> errorHandler, Action retryFaild, Action work)
        {
            do
            {
                try
                {
                    work();
                }
                catch (Exception ex)
                {
                    errorHandler(ex);
                    System.Threading.Thread.Sleep(retryDuration);
                    throw;
                }
            } while (retryCount-- > 0);
        }

        [DebuggerStepThrough]
        public AspectF Combine(Action<Action> newAspectDelegate)
        {
            if (this.Chain == null)
            {
                this.Chain = newAspectDelegate;
            }
            else
            {
                Action<Action> existingChain = this.Chain;
                Action<Action> callAnother = (work) =>
                    existingChain(() => newAspectDelegate(work));
                this.Chain = callAnother;
            }

            return this;
        }

        [DebuggerStepThrough]
        public void Do(Action work)
        {
            if (this.Chain == null)
            {
                work();
            }
            else
            {
                this.Chain(work);
            }
        }

        [DebuggerStepThrough]
        public TReturnType Return<TReturnType>(Func<TReturnType> work)
        {
            this.WorkDelegate = work;

            if (this.Chain == null)
            {
                return work();
            }
            else
            {
                TReturnType returnValue = default(TReturnType);
                this.Chain(() =>
                {
                    Func<TReturnType> workDelegate = WorkDelegate as Func<TReturnType>;
                    returnValue = workDelegate();
                });
                return returnValue;
            }
        }
    }
}