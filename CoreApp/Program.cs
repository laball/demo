using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.Models;
using Refit;

namespace CoreApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var settings = new RefitSettings
            {
                AuthorizationHeaderValueGetter = GetTokenHeadString//,
                //HttpMessageHandlerFactory = () => new HttpClientHandler()

                // () =>
                //{
                //    var request = new ClientCredentialsTokenRequest
                //    {
                //        Address = "http://10.27.225.184:5000" + "/api/token",
                //        ClientId = "snw_admin_client",
                //        ClientSecret = "snw_admin_secrect"
                //    };

                //    using (var client = new HttpClient())
                //    {
                //        var tokenResult = client.RequestClientCredentialsTokenAsync(request).Result;
                //        return Task.FromResult($"{tokenResult.TokenType} {tokenResult.AccessToken}");
                //    }
                //}
            };

            try
            {
                var dpsClient = CoreApp.RestService.For<IDpsClientService>("http://10.27.225.184:5000", settings);
                var dd = dpsClient.DiantuoHealth().Result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + ex.StackTrace);
            }

            //var client = new HttpClient();

            //var disco = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            //if (disco.IsError) throw new Exception(disco.Error);

            //var request = new ClientCredentialsTokenRequest
            //{
            //    Address = "http://localhost:5000/connect/token",
            //    ClientId = "client",
            //    ClientSecret = "secret",
            //    Scope = "api1"
            //};

            //var httpClient = new HttpClient();
            //var ttt = httpClient.RequestClientCredentialsTokenAsync(request).Result;

            //try
            //{
            //    var tokenService = RestService.For<ITokenService>("http://localhost:5000");
            //    var tttt = tokenService.GetToken("password", "dev_002", "E10ADC3949BA59ABBE56", "ro.client", "secret").Result;
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Trace.WriteLine(ex.Message + ex.StackTrace);
            //}




            String[] strs = new String[5];

            //var cts = new CancellationTokenSource();
            ////var bgtask = Task.Run(() => TestService.Run(cts.Token));

            //AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            //{
            //    Trace.WriteLine($"{DateTime.Now} 后台测试服务，准备进行资源清理！");

            //    cts.Cancel();    //设置IsCancellationRequested=true，让TestService今早结束 

            //    bgtask.Wait();   //等待 testService 结束执行

            //    Trace.WriteLine($"{DateTime.Now} 恭喜，Test服务程序已正常退出！");

            //    Environment.Exit(0);
            //};

            Test();



            Trace.WriteLine($"{DateTime.Now} 后端服务程序正常启动！");

            Console.ReadLine();
        }

        public static async Task<string> Get()
        {
            Trace.WriteLine($"{DateTime.Now.ToString("hh:mm:ss fff")} ThreadID: {Thread.CurrentThread.ManagedThreadId}");

            HttpClient client = new HttpClient();
            var res = await client.GetAsync("http://127.0.0.1:5000/api/values");
            return await res.Content.ReadAsStringAsync();
        }


        static async Task<string> GetTokenHeadString()
        {
            var request = new ClientCredentialsTokenRequest
            {
                Address = "http://10.27.225.184:5000" + "/api/token",
                ClientId = "snw_admin_client",
                ClientSecret = "snw_admin_secrect"
            };

            using (var client = new HttpClient())
            {
                var tokenResult = await client.RequestClientCredentialsTokenAsync(request);
                return tokenResult.AccessToken;
            }
        }


        static void Test()
        {
            for (int i = 0; i < 20; i++)
            {
                Task.Run(() =>
                {
                    var dd = Get().Result;
                    Trace.WriteLine($"{DateTime.Now.ToString("hh:mm:ss fff")} ThreadID: {Thread.CurrentThread.ManagedThreadId} " + dd);
                });
            }
        }



    }
}