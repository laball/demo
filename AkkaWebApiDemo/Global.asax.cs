using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Akka.Actor;
using Akka.Routing;
using AkkaWebApiDemo.Akka;

namespace AkkaWebApiDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ActorSystemRefs.ActorSystem = ActorSystem.Create("webapi");

            var actorSystem = ActorSystemRefs.ActorSystem;

            SystemActors.DemoActor = actorSystem.ActorOf<DemoActor>("demo");
        }

        protected void Application_End()
        {
            ActorSystemRefs.ActorSystem.Terminate();
        }

    }
}
