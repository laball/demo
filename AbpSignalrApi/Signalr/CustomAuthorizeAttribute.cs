using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AbpSignalrApi.Signalr
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
        {
            System.Diagnostics.Trace.WriteLine("AuthorizeHubConnection");

            return true;
        }

        public override bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            System.Diagnostics.Trace.WriteLine("AuthorizeHubMethodInvocation");
            return true;
        }

        protected override bool UserAuthorized(IPrincipal user)
        {
            System.Diagnostics.Trace.WriteLine("UserAuthorized");
            return true;
        }


    }
}