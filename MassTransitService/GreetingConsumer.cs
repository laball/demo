using System;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitService
{
    public class GreetingConsumer :IConsumer<GreetingCommand>
    {
        public async Task Consume(ConsumeContext<GreetingCommand> context)
        {
            await Console.Out.WriteLineAsync($"receive greeting commmand: {context.Message.Id},{context.Message.DateTime}");
        }
    }
}