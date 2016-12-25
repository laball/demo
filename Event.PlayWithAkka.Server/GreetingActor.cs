using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Event.PlayWithAkka.Common;

namespace Event.PlayWithAkka.Server
{
    public class GreetingActor : TypedActor, IHandle<GreetingMessage>
    {
        public void Handle(GreetingMessage message)
        {
            throw new InvalidOperationException();

            Console.WriteLine(string.Format("{0}  Hello world!", message.Name));
        }
    }
}