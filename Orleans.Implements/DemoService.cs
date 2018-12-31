using System.Diagnostics;
using System.Threading.Tasks;
using Orleans.Business;
using Orleans.Concurrency;
using Orleans.Interfaces;
using Orleans.Model;

namespace Orleans.Implements
{
    public class DemoService : Grain<User>, IDemoService
    {
        readonly IMessageService _messageService;

        public DemoService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public Task<Immutable<string>> SayHello()
        {
            Trace.WriteLine("DemoService ProcessID: " + Process.GetCurrentProcess().Id);

            //return Task.FromResult(_message);
            var message = _messageService.GetMessage();

            State.Name = message;
            State.Code = message;

            WriteStateAsync().Wait();

            return Task.FromResult(new Immutable<string>(message));
        }
    }

}
