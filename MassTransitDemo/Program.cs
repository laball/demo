using System;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitDemo
{
    internal class Program
    {
        private const string RabbitMQUser = "guest";
        private const string RabbitMQPassword = "guest";

        private static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username(RabbitMQUser);
                    h.Password(RabbitMQPassword);
                });

                config.ReceiveEndpoint(host, "queue_test", e =>
                {
                    e.Consumer(() => new MessageConsumer());
                });

                //config.ReceiveEndpoint(host, "queue_test", endPoint =>
                //{
                //    endPoint.Handler<MyMessage>(async context =>
                //    {
                //        await Console.Out.WriteLineAsync($"Received: {context.Message.Value}");
                //    });
                //});
            });

            using (bus.Start())
            {
                bus.Publish(new MyMessage { Value = "123" });
                Console.ReadLine();
            }
        }
    }

    internal class MyMessage
    {
        public string Value { get; set; }
    }

    internal class MessageConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            return Task.Run(() =>
            {
                System.Diagnostics.Trace.WriteLine(context.Message.Value);
            });
        }
    }
}