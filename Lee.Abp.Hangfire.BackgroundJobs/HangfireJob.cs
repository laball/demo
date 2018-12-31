using System;
using System.Diagnostics;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Lee.Abp.Application.Users;

namespace Lee.Abp.Hangfire.BackgroundJobs
{
    public class HangfireJob : BackgroundJob<int>, ITransientDependency
    {
        private IUserAppService _userAppService;

        public HangfireJob(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }


        public override void Execute(int args)
        {
            Trace.WriteLine($"Hangfire Test Job the time is {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}");
        }
    }
}
