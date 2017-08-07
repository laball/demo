using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;

namespace KafkaDemo
{
    class Program
    {
        static void Main(string[] args)
        {





        }

        static void SendMessage()
        {
            var options = new KafkaOptions(new Uri("http://127.0.0.1:8001"),new Uri("http://127.0.0.1:8002"));
            var router = new BrokerRouter(options);
            using (var client = new Producer(router))
            {
                client.SendMessageAsync("test", new[] { new Message("hello world") });
            }
        }

        static void ConsumeMessage()
        {
            var options = new KafkaOptions(new Uri("http://127.0.0.1:8001"), new Uri("http://127.0.0.1:8002"));
            var router = new BrokerRouter(options);
            var consumer = new Consumer(new ConsumerOptions("test", router));

            //Consume returns a blocking IEnumerable (ie: never ending stream)
            foreach (var message in consumer.Consume())
            {
                Console.WriteLine("Response: P{0},O{1} : {2}",
                    message.Meta.PartitionId, message.Meta.Offset, message.Value);
            }
        }

    }
}
