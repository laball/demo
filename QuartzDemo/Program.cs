using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using System.Diagnostics;

namespace QuartzDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler();

            //string EVERY_HOUR = "0 */30 * * * ?";
            string EVERY_HOUR = string.Format("0 {0}/30 * * * ?",DateTime.Now.Minute + 1);

            var time = DateTime.Now.AddSeconds(5);
            //var cron = string.Format("{0} {1} {2} * * ? *",time.Second,time.Minute,time.Hour);
            var cron = string.Format("{0} {1} * * * ? *",time.Second,time.Minute);
            Trace.WriteLine(cron);
            //var after5Minite = DateTime.Now.AddMinutes(1);
            //var cron = string.Format("0 {0} * * * ?",after5Minite.Minute);

            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(EVERY_HOUR)
                .WithPriority(1)
                .Build();

            var mainJob = JobBuilder.Create<MainJob>().Build();
            scheduler.ScheduleJob(mainJob,trigger);

           // var trigger = TriggerBuilder.Create()
           //.WithCronSchedule("0 46 13 27 5 ?")//5s
           //.WithPriority(1)
           //.Build();

           // var mainJob = JobBuilder.Create<MainJob>().Build();
           // scheduler.ScheduleJob(mainJob,trigger);
            scheduler.Start();

            Console.ReadLine();
        }
    }
}
