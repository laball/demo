using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class SubJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                Trace.WriteLine(string.Format("SubJob Execute {0}, key :{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), context.JobDetail.JobDataMap["key"]));
            });

        }
    }
}
