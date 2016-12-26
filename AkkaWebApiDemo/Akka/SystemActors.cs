using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Akka.Actor;

namespace AkkaWebApiDemo.Akka
{
    public class SystemActors
    {
        public static IActorRef DemoActor = ActorRefs.Nobody;
    }
}