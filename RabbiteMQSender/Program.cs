using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RabbiteMQSender
{
    static class Program
    {
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



                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange:"",
                                     routingKey:"hello",
                                     basicProperties:null,
                                     body:body);
                Console.WriteLine(" [x] Sent {0}",message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        public static byte[] Serialize(this object obj)
        {
            using(var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream,obj);
                return stream.ToArray();
            }
        }
    }
}
