using System.Collections.Generic;
using System.Web.Http;
using Abp.WebApi.Controllers;
using AbpSignalrApi.Signalr;
using Microsoft.AspNet.SignalR;
using Microsoft.Web.Http;

namespace AbpSignalrApi.Controllers
{
    [ApiGroup("测试", "测试")]
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:version}/[controller]")]
    public class ValuesController : AbpApiController
    {
        private readonly IDispatcher dispatcher;

        public ValuesController(IDispatcher dispatcher)
        //public ValuesController(IHubContext<IDispatcher> hubContext)
        {
            this.dispatcher = dispatcher;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            dispatcher.SendMessage(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
