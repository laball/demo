using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.RealTime;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;

namespace AbpSignalrApi.Signalr
{
    /// <summary>
    /// 官方文档：https://docs.microsoft.com/zh-cn/aspnet/signalr/
    /// 
    /// ABP 中文文档
    /// http://www.cnblogs.com/farb/p/ABPTheory.html
    /// http://www.cnblogs.com/mienreal/p/4528470.html
    /// 
    /// 覆盖的代码参考自最新的Abp signalr框架代码
    /// https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/Runtime/Session/ClaimsAbpSession.cs
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    /// <seealso cref="Abp.Dependency.ISingletonDependency" />
    [CustomAuthorize]
    public class MyChatHub : Hub, ISingletonDependency
    {
        private readonly IOnlineClientManager _onlineClientManager;

        public IAbpSession AbpSession { get; set; }
        public ILogger Logger { get; set; }

        public MyChatHub(IOnlineClientManager onlineClientManager)
        {
            _onlineClientManager = onlineClientManager;

            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;

            System.Diagnostics.Trace.WriteLine("MyChatHub ctor");
        }

        public void SendMessage(string message)
        {
            Clients.All.getMessage(string.Format("User {0}: {1}", AbpSession?.UserId, message));
        }

        //public async override Task OnConnected()
        //{
        //    await base.OnConnected();

        //    Logger.Debug("A client connected to MyChatHub: " + Context.ConnectionId);
        //}

        //public async override Task OnDisconnected(bool stopCalled)
        //{
        //    await base.OnDisconnected(stopCalled);
        //    Logger.Debug("A client disconnected from MyChatHub: " + Context.ConnectionId);
        //}

        public override async Task OnConnected()
        {
            await base.OnConnected();

            //可将授权信息写入Cookie中，再在Hub中解析，建立用户与ConnectId之间的关联关系；
            foreach (var item in Context.RequestCookies.Values)
            {
                Logger.Debug("Cookie  " + item.Name + " : " + item.Value);
            }

            //$.connection.hub.qs = {"qs_token":"qs_token_value"};
            //在JS中使用以上代码设置即可；
            foreach (var item in Context.QueryString)
            {
                Logger.Debug("QueryString  " + item.Key + " : " + item.Value);
            }

            //see https://stackoverflow.com/questions/15528221/passing-token-through-http-headers-signalr
            //从文章看，WebSocket是不支持自定义Http Headder
            //但.net 版本的signalr是支持Header的
            foreach (var item in Context.Headers)
            {
                Logger.Debug("Heanders  " + item.Key + " : " + item.Value);
            }

            var client = CreateClientForCurrentConnection();

            Logger.Debug("A client is connected: " + client);

            _onlineClientManager.Add(client);
        }

        public override async Task OnReconnected()
        {
            await base.OnReconnected();

            var client = _onlineClientManager.GetByConnectionIdOrNull(Context.ConnectionId);
            if (client == null)
            {
                client = CreateClientForCurrentConnection();
                _onlineClientManager.Add(client);
                Logger.Debug("A client is connected (on reconnected event): " + client);
            }
            else
            {
                Logger.Debug("A client is reconnected: " + client);
            }
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);

            Logger.Debug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                _onlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        private IOnlineClient CreateClientForCurrentConnection()
        {
            return new OnlineClient(
                Context.ConnectionId,
                GetIpAddressOfClient(),
                AbpSession.TenantId,//未开启多租户时，TenantId默认为1，参见
                AbpSession.UserId ?? int.Parse(Context.RequestCookies["userID"].Value)
            );
            //此处利用前端将用户信息写入Cookie中，SignalR连接时，即可获取用户信息和ConnectionId
            //形成映射关系
        }

        private string GetIpAddressOfClient()
        {
            try
            {
                return Context.Request.Environment["server.RemoteIpAddress"].ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("Can not find IP address of the client! connectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }

    }
}