using System;
using System.Threading.Tasks;
using Core.Dao;
using Core.Entity;
using log4net;
using Quartz;

namespace WebApplication1.Quartz
{
    public class DemoJob : IJob
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DemoJob));

        private readonly IRepository<User> _userRepo;

        public DemoJob(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var batchCount = context.JobDetail.JobDataMap.GetInt("BatchCountKey");
                log.Info($"job data test BatchCount:{batchCount}");

                var user = _userRepo.Single(c => c.RoleID > 0);
                log.Info($"ioc in job test UserName:{user.Name}");

                log.Info($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}");
            });
        }
    }
}
