using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;

namespace KafkaDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseConfluentKafkaProducer();

            Thread.Sleep(5000);

            UseConfluentKafkaConsumer();

            Console.ReadLine();
        }


        static void UseConfluentKafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                producer.Produce("iris", new Message<Null, string> { Value = "11@qq" });
            }
        }

        static void UseConfluentKafkaConsumer()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("iris");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }


        static void SendMessage()
        {
            var options = new KafkaOptions(new Uri("http://127.0.0.1:8001"), new Uri("http://127.0.0.1:8002"));
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
