using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RabbiteMQReceiver
{
    static class Program
    {
        public const string ExchangeName = "test.exchange";
        public const string QueueName = "test.queue";

        public static ConnectionFactory Factory;
        public static IConnection Connection;
        public static IModel ReceiveChannel;

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost",UserName = "root",Password = "root",VirtualHost = "/" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue:"hello",
                                     durable:false,
                                     exclusive:false,
                                     autoDelete:false,
                                     arguments:null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model,ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}",message);
                };
                channel.BasicConsume(queue:"hello",
                                     noAck:true,
                                     consumer:consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            //Initialize();

            //ReceiveChannel.ExchangeDeclare(ExchangeName,"direct",true,false,null);
            //ReceiveChannel.QueueDeclare(QueueName,true,false,false,null);
            //ReceiveChannel.QueueBind(QueueName,ExchangeName,"");

            //var consumer = new EventingBasicConsumer(ReceiveChannel);
            //consumer.Received += consumer_Received;

            //Console.ReadLine();
        }


        public static T Deserialize<T>(this byte[] byteData)
        {
            using(var stream = new MemoryStream(byteData,false))
            {
                IFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        static void consumer_Received(object sender,BasicDeliverEventArgs e)
        {
            using(var stream = new MemoryStream(e.Body,false))
            {
                IFormatter formatter = new BinaryFormatter();
                var message = (MQMessage)formatter.Deserialize(stream);
                Trace.WriteLine(string.Format("ID:{0},Name:{1}",message.ID,message.Name));
            }
        }

        public static void Initialize()
        {
            Factory = new ConnectionFactory { HostName = "localhost",UserName = "root",Password = "root",VirtualHost = "/" };
            Connection = Factory.CreateConnection();
            ReceiveChannel = Connection.CreateModel();
        }

    }
}
