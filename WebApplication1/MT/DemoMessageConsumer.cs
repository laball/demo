using MassTransit;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebApplication1.MT
{
    public class DemoMessageConsumer : IConsumer<IDemoMessage>
    {
        public Task Consume(ConsumeContext<IDemoMessage> context)
        {
            return Task.Factory.StartNew(() =>
            {
                Trace.WriteLine($"DemoMessage ID:{context.Message.ID},Name:{context.Message.Name}");
            });
        }
    }
}
