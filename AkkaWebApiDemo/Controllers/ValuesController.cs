using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;
using AkkaWebApiDemo.Akka;

namespace AkkaWebApiDemo.Controllers
{
    //[Authorize]

    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
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
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        // GET api/values/{name}

        [Route("GetName/{name}")]
        public async Task<string> GetName(string name)
        {
            var str = await SystemActors.DemoActor.Ask<string>(new DemoMessage {Name = name } , TimeSpan.FromSeconds(1));
            return str;
        }

    }
}
