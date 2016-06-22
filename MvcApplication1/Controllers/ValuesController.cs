using MvcApplication1.Attribute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class ValuesController :ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/GetStrings
        public async Task<IEnumerable<string>> GetStrings()
        {
            return await Task.Factory.StartNew(() =>
            {
                return new string[] { "111","222","333" };
            });
        }

        // GET api/values/5
        public async Task<string> Get(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var session = NHibernateSessionContext.SessionFactory.GetCurrentSession();
                    var items = session.QueryOver<Needredorequest>().List();


                    Trace.WriteLine(string.Format("Session:{0}",session == null ? -12345678 : session.GetHashCode()));
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(ex.StackTrace);
                }

                return DateTime.Now.ToString("yyyy-MM-dd HH:24:mm:ss");
            });
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id,[FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }

    }
}