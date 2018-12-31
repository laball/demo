using Autofac;
using GreenPipes;
using MassTransit;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitDemo
{
    internal class Program
    {
        private const string RabbitMQUser = "guest";
        private const string RabbitMQPassword = "guest";

        private static void Main(string[] args)
        {
            AutofacTest();

            Console.ReadLine();
        }


        public static void Test1()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username(RabbitMQUser);
                    h.Password(RabbitMQPassword);
                });

                config.Durable = true;
                config.UseRateLimit(10, TimeSpan.FromSeconds(1));
                config.UseConcurrencyLimit(100);
                //config.UseRetry(Retry.Interval(3, TimeSpan.FromMinutes(1)));

                config.ReceiveEndpoint(host, "queue_test", e =>
                {
                    e.Consumer(() => new MessageConsumer(), x =>
                    {

                    });
                });
            });

            bus.Start();

            var rd = new Random();
            for (int i = 0; i < 100; i++)
            {
                bus.Publish(new MyMessage { Value = rd.Next(100, 999).ToString() });

                Thread.Sleep(100);
            }
        }


        public static void AutofacTest()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<MessageConsumer>();
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());

            builder.Register(contex => BuildBusControl(contex)).SingleInstance()
            .As<IBusControl>()
            .As<IBus>();

            var container = builder.Build();

            var bc = container.Resolve<IBusControl>();
            bc.Start();

            Task.Factory.StartNew(() =>
            {
                var rd = new Random();
                for (int i = 0; i < 10; i++)
                {
                    bc.Publish(new MyMessage { Value = rd.Next(100, 999).ToString() });

                    Thread.Sleep(1000);
                }
            });

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    bc.Publish(new DemoMessage(i, "Lee"));

                    Thread.Sleep(1000);
                }
            });           
        }

        private static IBusControl BuildBusControl(IComponentContext context)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username(RabbitMQUser);
                    h.Password(RabbitMQPassword);
                });

                config.Durable = true;
                config.UseRateLimit(2, TimeSpan.FromSeconds(1));
                config.UseConcurrencyLimit(100);
                //config.UseRetry(Retry.Interval(3, TimeSpan.FromMinutes(1)));

                config.ReceiveEndpoint(host, "queue_test", e =>
                {
                    e.LoadFrom(context);
                });
            });

            return bus;
        }

    }




    internal class MyMessage
    {
        public string Value { get; set; }
    }


    internal class DemoMessage
    {
        public int ID { get; }
        public string Name { get; }

        public DemoMessage(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    class DemoMessageConsume : IConsumer<DemoMessage>
    {
        public Task Consume(ConsumeContext<DemoMessage> context)
        {
            return Task.Run(() =>
            {
                Trace.WriteLine($"DemoMessage: {context.Message.ID},{context.Message.Name}");
            });
        }
    }

    internal class MessageConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            return Task.Run(() =>
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff") + "   " + context.Message.Value);
            });
        }
    }
}