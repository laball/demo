using System.Threading;

namespace Summary.Framework.Task
{
    public class AsyncVoid
    {
        public async void Test()
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
            });
        }
    }
}