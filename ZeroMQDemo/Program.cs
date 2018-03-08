using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace ZeroMQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                Publisher(args);
            });



            Task.Factory.StartNew(() =>
            {
                Subscriber(args);
            });


            Console.ReadLine();
        }

        static void Publisher(string[] args)
        {
            //
            // Pathological publisher
            // Sends out 1,000 topics and then one random update per second
            //
            // Author: metadings
            //

            if (args == null || args.Length < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Usage: ./{0} PathoPub [Endpoint]", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine();
                Console.WriteLine("    Endpoint  Where PathoPub should connect to.");
                Console.WriteLine("              Default is null, Binding on tcp://*:5556");
                Console.WriteLine();
                args = new string[] { null };
            }

            using (var context = new ZContext())
            using (var publisher = new ZSocket(context, ZSocketType.PUB))
            {
                if (args[0] != null)
                {
                    publisher.Connect(args[0]);
                }
                else
                {
                    publisher.Bind("tcp://*:5556");
                }

                // Ensure subscriber connection has time to complete
                Thread.Sleep(1000);

                // Send out all 1,000 topic messages
                for (int topic = 0; topic < 1000; ++topic)
                {
                    publisher.SendMore(new ZFrame(string.Format("{0:D3}", topic)));
                    publisher.Send(new ZFrame("Save Roger" + topic));
                }

                // Send one random update per second
                var rnd = new Random();
                while (true)
                {
                    Thread.Sleep(1000);
                    //publisher.SendMore(new ZFrame(string.Format("{0:D3}", rnd.Next(1000))));
                    publisher.SendMore(new ZFrame(string.Format("{0:D3}", 124)));
                    publisher.Send(new ZFrame("Off with his head!"));
                }
            }
        }

        static void Subscriber(string[] args)
        {
            //
            // Pathological subscriber
            // Subscribes to one random topic and prints received messages
            //
            // Author: metadings
            //

            if (args == null || args.Length < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Usage: ./{0} PathoSub [Endpoint]", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine();
                Console.WriteLine("    Endpoint  Where PathoSub should connect to.");
                Console.WriteLine("              Default is tcp://127.0.0.1:5556");
                Console.WriteLine();
                args = new string[] { "tcp://127.0.0.1:5556" };
            }

            using (var context = new ZContext())
            using (var subscriber = new ZSocket(context, ZSocketType.SUB))
            {
                subscriber.Connect(args[0]);

                var rnd = new Random();
                //var subscription = string.Format("{0:D3}", rnd.Next(1000));
                var subscription = string.Format("{0:D3}", 124);
                subscriber.Subscribe(subscription);

                ZMessage msg;
                ZError error;
                while (true)
                {
                    if (null == (msg = subscriber.ReceiveMessage(out error)))
                    {
                        if (error == ZError.ETERM)
                            break;    // Interrupted
                        throw new ZException(error);
                    }
                    using (msg)
                    {
                        if (msg[0].ReadString() != subscription)
                        {
                            throw new InvalidOperationException();
                        }


                        Trace.WriteLine(msg.Count);
                        Trace.WriteLine(msg[0].ReadString());
                        Trace.WriteLine(msg[1].ReadString());
                    }
                }
            }
        }
    }
}
