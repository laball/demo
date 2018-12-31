using System;
using System.Collections.Concurrent;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Core.Integrated
{
    /// <summary>
    /// 使用.net core内置IOC实现IJobFactory
    /// see 
    /// https://stackoverflow.com/questions/42157775/net-core-quartz-dependency-injection
    /// https://github.com/alphacloud/Autofac.Extras.Quartz/blob/master/src/Autofac.Extras.Quartz/QuartzAutofacFactoryModule.cs
    /// </summary>
    public class JobFactory : IJobFactory, IDisposable
    {
        private readonly IServiceProvider _container;

        internal ConcurrentDictionary<object, JobTrackingInfo> RunningJobs { get; } = new ConcurrentDictionary<object, JobTrackingInfo>();

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            RunningJobs.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public JobFactory(IServiceProvider container)
        {
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (bundle == null) throw new ArgumentNullException(nameof(bundle));
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

            var jobType = bundle.JobDetail.JobType;

            var nestedScope = _container.CreateScope();

            IJob newJob;
            try
            {
                newJob = (IJob)nestedScope.ServiceProvider.GetService(jobType);
                var jobTrackingInfo = new JobTrackingInfo(nestedScope);
                RunningJobs[newJob] = jobTrackingInfo;
                nestedScope = null;
            }
            catch (Exception ex)
            {
                if (nestedScope != null)
                {
                    nestedScope?.Dispose();
                }

                throw new SchedulerConfigException(string.Format(CultureInfo.InvariantCulture,
                    "Failed to instantiate Job '{0}' of type '{1}'",
                    bundle.JobDetail.Key, bundle.JobDetail.JobType), ex);
            }
            return newJob;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            //see https://stackoverflow.com/questions/42199592/quartz-net-dependency-injection-net-core
            //(job as IDisposable)?.Dispose();

            if (job == null)
                return;

            if (!RunningJobs.TryRemove(job, out var trackingInfo))
            {
                (job as IDisposable)?.Dispose();
            }
            else
            {
                trackingInfo.Scope?.Dispose();
            }
        }

        /// <summary>
        /// 包一层的设计思想，应该是为了减低代码的侵入性
        /// 一旦依赖的外部接口变化，则只需要修改创建Scope创建，以及JobTrackingInfo类即可
        /// 尽量减小由于外部表接口变更导致的代码变动
        /// </summary>
        internal sealed class JobTrackingInfo
        {
            public JobTrackingInfo(IServiceScope scope) => Scope = scope;

            public IServiceScope Scope { get; }
        }
    }
}
