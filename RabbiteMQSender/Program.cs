using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RabbiteMQSender
{
    static class Program
    {
        private static ConnectionFactory factory;
        private static IConnection connection;
        private static IModel channel;

        private static int sID;

        static void Main(string[] args)
        {
            Initialize();

            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var task = Task.Factory.StartNew(SendMessagePerSecond);
            task.ContinueWith((tsk) =>
            {
                Initialize();
            });

            //UnInitialize();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static void SendMessagePerSecond()
        {
            while (true)
            {
                var message = new RabbiteMQReceiver.MQMessage() { ID = sID, Name = "Lee", CreateTime = DateTime.Now };
                var body = message.Serialize();

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

                sID++;

                Thread.Sleep(1000);
            }
        }

        static void Initialize()
        {
            factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "root",
                Password = "root",
                VirtualHost = "/"
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        static void UnInitialize()
        {
            channel.Close();
            connection.Close();
        }

        public static byte[] Serialize(this object obj)
        {
            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
    }
}
