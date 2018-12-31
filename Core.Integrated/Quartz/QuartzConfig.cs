using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.Quartz;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Core.Integrated
{
    /// <summary>
    /// Quartz默认使用quartz.config文作为配置文件，INI格式，可在<seealso cref="Environment"/>中重置
    /// 3.0.4版本已经将原有动态库拆分，需要根据需要引用其他包；
    /// 支持集群，尚未研究
    /// </summary>
    public static class QuartzConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseQuartz(this IServiceCollection services, Func<IServiceProvider, IJobFactory> createJobFactory, params Assembly[] jobAssemblies)
        {
            services.AddSingleton(c => BuildScheduler(c, createJobFactory));

            foreach (var assembly in jobAssemblies)
            {
                services.AddJob(assembly);
            }

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="applicationLifetime"></param>
        public static void ConfigQuartz(this IApplicationBuilder app, IServiceProvider serviceProvider, IApplicationLifetime applicationLifetime)
        {
            var scheduler = serviceProvider.GetService<IScheduler>();
            applicationLifetime.ApplicationStarted.Register(() => AsyncContext.Run(() => scheduler.Start()));
            applicationLifetime.ApplicationStopped.Register(() => AsyncContext.Run(() => scheduler.Shutdown()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddJob(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var jobType = typeof(IJob);
            var jobTypes = from t in assembly.GetExportedTypes()
                           where jobType.IsAssignableFrom(t) && t.IsAbstract == false && t.IsClass == true
                           select t;

            foreach (var type in jobTypes)
            {
                services.AddScoped(type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IScheduler BuildScheduler(IServiceProvider serviceProvider)
        {
            //重置Quartz默认配置文件名称（默认为quartz.config）
            Environment.SetEnvironmentVariable(StdSchedulerFactory.PropertiesFile, "quartz.ini");

            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.JobFactory = new JobFactory(serviceProvider);

            //通过代码调度Job
            //var voteJob = JobBuilder.Create<DemoJob>()
            // .Build();

            //var voteJobTrigger = TriggerBuilder.Create()
            //    .StartNow()
            //    .WithSimpleSchedule(s => s
            //        .WithIntervalInSeconds(2)
            //        .RepeatForever())
            //    .Build();

            //scheduler.ScheduleJob(voteJob, voteJobTrigger).Wait();

            return scheduler;
        }


        public static IScheduler BuildScheduler(IServiceProvider serviceProvider, Func<IServiceProvider, IJobFactory> createJobFactory)
        {
            //重置Quartz默认配置文件名称（默认为quartz.config）
            Environment.SetEnvironmentVariable(StdSchedulerFactory.PropertiesFile, "quartz.ini");

            var schedulerFactory = new StdSchedulerFactory();

            var scheduler = schedulerFactory.GetScheduler()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            scheduler.JobFactory = createJobFactory(serviceProvider);

            return scheduler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        public static IScheduler BuildScheduler(ILifetimeScope lifetimeScope)
        {
            //重置Quartz默认配置文件名称（默认为quartz.config）
            Environment.SetEnvironmentVariable(StdSchedulerFactory.PropertiesFile, "quartz.ini");

            var schedulerFactory = new StdSchedulerFactory();

            var scheduler = schedulerFactory.GetScheduler()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            scheduler.JobFactory = new AutofacJobFactory(lifetimeScope, "quartz.job");

            return scheduler;
        }

    }
}