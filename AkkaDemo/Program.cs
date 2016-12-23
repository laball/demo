using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDemo
{

    public class GreetingMessage
    {
        public string Name { get; set; }
    }

    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<GreetingMessage>(c => { Console.WriteLine("Hello " + c.Name); });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("Test");

            var greeter = system.ActorOf<GreetingActor>();
            greeter.Tell(new GreetingMessage() { Name = "Lee" });

            Console.ReadLine();
        }
    }
}
