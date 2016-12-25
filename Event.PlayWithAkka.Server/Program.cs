using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;

namespace Event.PlayWithAkka.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
                akka {
                    actor {provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""}
                    remote {
                        helios.tcp {
                                    transport-class = ""Akka.Remote.Transport.Helios.HeliosTcpTransport,Akka.Remote""
                                    applied-adapers = []
                                    transport-protocol = tcp
                                    port = 8081
                                    hostname = localhost
                                    }
                            }
                    }
            ");

            using (var system = ActorSystem.Create("MyServer", config))
            {
                system.ActorOf<GreetingActor>("Greeting");

                Console.ReadLine();
            }
        }
    }
}