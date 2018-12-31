using Abp;
using Abp.Dependency;
using Abp.Notifications;
using Abp.RealTime;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;

namespace AbpSignalrApi.Signalr
{
    public class MyChatHubDispatcher : IDispatcher, ITransientDependency
    {
        private readonly IRealTimeNotifier realTimeNotifier;
        private readonly IOnlineClientManager onlineClientManager;

        public IAbpSession AbpSession { get; set; }

        public ILogger Logger { get; set; }

        public MyChatHubDispatcher(
            IRealTimeNotifier realTimeNotifier,
            IOnlineClientManager onlineClientManager)
        {
            this.realTimeNotifier = realTimeNotifier;
            this.onlineClientManager = onlineClientManager;

            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
        }

        public void SendMessage(string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyChatHub>();

            //未开启多租户时，默认为1；
            //see:Abp.Runtime.Session.ClaimsAbpSession 实现
            var user = new UserIdentifier(1, AbpSession.GetUserId());
            var userClients = onlineClientManager.GetAllByUserId(user);

            foreach (var userClient in userClients)
            {
                hubContext.Clients.Client(userClient.ConnectionId).getMessage(string.Format("User {0}: {1}", AbpSession?.UserId, message));
                Logger.Debug($"TenantId: {userClient.Properties},UserId: {userClient.UserId},IP: {userClient.IpAddress},ConnectionId: {userClient.ConnectionId}");
            }
        }
    }
}