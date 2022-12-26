using Microsoft.AspNetCore.Mvc;
using Summary.Net7.WebAPI.Dtos;
using Summary.Net7.WebAPI.Filters;

namespace Summary.Net7.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(MyExceptionFilterAttribute), IsReusable = true)]
    [ServiceFilter(typeof(MyActionFilterAttribute), IsReusable = true)]
    public class BaseController : ControllerBase
    {
        protected virtual ActionResult<CommonResultDto<T>> Ok<T>(T data, string message = "", string moreMessages = "")
        {
            var result = new CommonResultDto<T>
            {
                Data = data,
                Code = 200,
                Message = message,
                MoreMessages = moreMessages
            };

            return result;
        }

        protected virtual ActionResult<CommonResultDto<T>> Error<T>(T data, string message = "", string moreMessages = "")
        {
            var result = new CommonResultDto<T>
            {
                Data = data,
                Code = 500,
                Message = message,
                MoreMessages = moreMessages
            };

            return result;
        }
    }
}