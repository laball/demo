using System;
using Akka.Actor;
using Akka.Configuration;
using Event.PlayWithAkka.Common;

namespace Event.PalyWithAkka.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
            akka{
            actor{
                provider = ""Akka.Remote.RemoteActorRefProvider,Akka.Remote""
            }

            remote{
                helios.tcp{
                    transprot-class= ""Akka.Remote.Transport.Helios.HeliosTcpTransport,Akka.Remote""
                    applied-adapters = []
                    transprot-protocol = tcp
                    port = 0
                    hostname = localhost
                }
            }
            }");

            using (var system = ActorSystem.Create("MyClient", config))
            {
                var greeting = system.ActorSelection("akka.tcp://MyServer@localhost:8081/user/Greeting");
                while (true)
                {
                    var input = Console.ReadLine();
                    //if (input.Equals("SayHello"))
                    {
                        greeting.Tell(new GreetingMessage { Name = input });
                    }
                }
            }
        }
    }
}