using System.Threading;

namespace Summary.Framework.Task
{
    public class AwaitTask
    {
        public async System.Threading.Tasks.Task Test()
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
            });
        }
    }
}