using System.Collections.Generic;
using Core.Integrated;
using Lee.Abp.Web.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Lee.Abp.Web.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiGroup("通用", "测试")]
    public class ValuesController : ApiControllerBase
    {
        [HttpGet]
        public HttpResponse<IEnumerable<string>> Get()
        {
            return Success(new string[] { "value1", "value2" } as IEnumerable<string>);
        }

        [HttpGet("{id}")]
        public HttpResponse<int> Get(int id)
        {
            return Success(id);
        }
    }
}
