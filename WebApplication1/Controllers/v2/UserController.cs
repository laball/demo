using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.v2
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiGroup("通用", "用户信息")]
    public class UserController : Controller
    {
        [HttpPost]
        public int CreateUser()
        {
            return 1;
        }

        [HttpDelete]
        [Route("{userID}")]
        public int DeleteByID(int userID)
        {
            return 1;
        }
    }
}