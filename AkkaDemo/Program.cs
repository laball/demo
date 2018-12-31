using System;
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
            Receive<GreetingMessage>(HanleGreetingMessage, null);
        }

        protected void HanleGreetingMessage(GreetingMessage message)
        {
            Console.WriteLine(string.Format("Greeting :{0}", message.Name));
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var system = ActorSystem.Create("Test");

            var greeter = system.ActorOf<GreetingActor>();
            greeter.Tell(new GreetingMessage() { Name = "Lee" });

            Console.ReadLine();
        }
    }
}