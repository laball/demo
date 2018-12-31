using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.MT;

namespace WebApplication1.Controllers.v1
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiGroup("通用", "消息队列")]
    public class MQDemoController : Controller
    {
        private readonly IBusControl busControl;

        public MQDemoController(IBusControl busControl)
        {
            this.busControl = busControl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<bool> PostMessage()
        {
            var rd = new Random();
            var endPoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/rbmq_test"));
            await endPoint.Send<IDemoMessage>(new
            {
                ID = rd.Next(111, 999),
                Name = "Lee"
            });

            endPoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/rbmq_test_1"));
            await endPoint.Send<ICreateUserMessage>(new
            {
                UserName = "Lee",
                Mail = "laball1573@163.com"
            });

            return await Task.FromResult(true);
        }

    }
}