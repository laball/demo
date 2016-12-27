using Akka.Actor;

namespace AkkaWebApiDemo.Akka
{
    public class DemoActor : ReceiveActor
    {
        public DemoActor()
        {
            //Receive<DemoMessage>(message =>
            //{
            //    var msg = string.Format("Hello {0}", message.Name);
            //    Sender.Tell(msg, Self);
            //});

            Receive<DemoMessage>(Handle, e => { return !string.IsNullOrEmpty(e.Name); });
        }

        private void Handle(DemoMessage message)
        {
            var msg = string.Format("Hello {0}", message.Name);
            Sender.Tell(msg, Self);
        }
    }
}