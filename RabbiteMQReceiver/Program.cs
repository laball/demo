using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace RabbiteMQReceiver
{
    internal static class Program
    {
        public const string ExchangeName = "test.exchange";
        public const string QueueName = "test.queue";

        public static ConnectionFactory Factory;
        public static IConnection Connection;
        public static IModel ReceiveChannel;

        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = "amqp://root:root@localhost:15672//",
                AutomaticRecoveryEnabled = true
            };

            var connection = factory.CreateConnection();
            connection.ConnectionShutdown += (s, e) =>
            {
                Trace.WriteLine(string.Format("{0} {1}", s.GetType().FullName, e.Cause));
            };

            var channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            //var factory = new ConnectionFactory { HostName = "localhost", UserName = "root", Password = "root", VirtualHost = "/" };
            //var factory = new ConnectionFactory { HostName = "10.50.50.22", UserName = "hzadmin", Password = "a1234567", VirtualHost = "/" };

            //exchange type: fanout,direct,topic,headers
            //fanout:广播,消息后会将消息广播给所有绑定到它上面的队列

            channel.ExchangeDeclare("test", "direct", true);

            //durable:持久化
            //exclusive:程序退出后被自动删除
            channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
            //channel.BasicQos();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                //var message = Encoding.UTF8.GetString(body);

                var message = body.Deserialize<MQMessage>();

                Trace.WriteLine(string.Format("ID:{0},Name:{1},CreateTime:{2}", message.ID, message.Name, message.CreateTime.ToString("yyyy-MM-dd HH:mm:ss fff")));
            };

            channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            //Initialize();

            //ReceiveChannel.ExchangeDeclare(ExchangeName,"direct",true,false,null);
            //ReceiveChannel.QueueDeclare(QueueName,true,false,false,null);
            ReceiveChannel.QueueBind(QueueName, ExchangeName, "");

            //var consumer = new EventingBasicConsumer(ReceiveChannel);
            //consumer.Received += consumer_Received;

            //Console.ReadLine();
        }

        public static T Deserialize<T>(this byte[] byteData)
        {
            using (var stream = new MemoryStream(byteData, false))
            {
                IFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        private static void Inite()
        {
            RabbiteMQSection MqSection = ConfigurationManager.GetSection("RabbitMQ") as RabbiteMQSection;

            var factory = new ConnectionFactory { Uri = "" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            foreach (ExchangeElement exchange in MqSection.Exchanges)
            {
                IDictionary<string, object> declareArgDic = new Dictionary<string, object>();
                foreach (ArgumentElement dArg in exchange.Arguments ?? new ArgumentElementCollection())
                {
                    declareArgDic[dArg.Key] = dArg.Value;
                }

                channel.ExchangeDeclare(exchange.Exchange, exchange.Type, exchange.Durable, exchange.AutoDelete, declareArgDic);

                //if (exchange.Bind != null)
                //{
                //    if (exchange.Bind.Arguments != null)
                //    {
                //        IDictionary<string, object> bindArgDic = new Dictionary<string, object>();

                //        foreach (ArgumentElement bArg in exchange.Bind.Arguments)
                //        {
                //            bindArgDic[bArg.Key] = bArg.Value;
                //        }

                //        channel.ExchangeBind(exchange.Bind.Destination, exchange.Bind.Source, exchange.Bind.RoutingKey, bindArgDic);
                //    }
                //    else
                //    {
                //        channel.ExchangeBind(exchange.Bind.Destination, exchange.Bind.Source, exchange.Bind.RoutingKey);
                //    }
                //}
            }

            foreach (QueueElement queue in MqSection.Queues)
            {
            }
        }

        private static void consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            using (var stream = new MemoryStream(e.Body, false))
            {
                IFormatter formatter = new BinaryFormatter();
                var message = (MQMessage)formatter.Deserialize(stream);
                Trace.WriteLine(string.Format("ID:{0},Name:{1}", message.ID, message.Name));
            }
        }

        public static void Initialize()
        {
            Factory = new ConnectionFactory { HostName = "localhost", UserName = "root", Password = "root", VirtualHost = "/" };
            Connection = Factory.CreateConnection();
            ReceiveChannel = Connection.CreateModel();
        }
    }
}