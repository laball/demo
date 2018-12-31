using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Quartz;
using Lee.Abp.Application.Users;
using Quartz;

namespace Lee.Abp.Quartz.BackgroundJobs
{
    /// <summary>
    /// https://yq.aliyun.com/articles/314544
    /// </summary>
    public class QuartzJob : JobBase, ITransientDependency
    {
        private IUserAppService _userAppService;

        public QuartzJob(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public override Task Execute(IJobExecutionContext context)
        {
            Trace.WriteLine($"Quartz Test Job the time is {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}");
            return Task.CompletedTask;
        }
    }
}
