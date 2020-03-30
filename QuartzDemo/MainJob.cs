using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class MainJob :IJob
    {
        static int key = 0;

        int time = 0;


        public Task Execute(IJobExecutionContext context)
        {
            key++;

            int? time;
            if(context.JobDetail.JobDataMap.Contains("time"))
            {
                time = context.JobDetail.JobDataMap.GetIntValue("time");
                time++;
                context.JobDetail.JobDataMap["time"] = time;
            }
            else
            {
                time = 1;
                context.JobDetail.JobDataMap["time"] = time;
            }

            Trace.WriteLine(string.Format("time:{0}",time));




            //Trace.WriteLine(string.Format("MainJob Execute {0}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

            //var jobDetail = JobBuilder.Create<SubJob>().UsingJobData("key",key).Build();

            //var trigger = TriggerBuilder.Create()
            //           .WithCronSchedule("*/5 * * * * ?")//5s
            //           .WithPriority(1)
            //           .Build();

            //context.Scheduler.ScheduleJob(jobDetail,trigger);

            return  Task.CompletedTask;
        }
    }
}
