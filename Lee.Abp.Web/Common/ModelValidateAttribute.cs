using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Lee.Abp.Web.Common
{
    /// <summary>
    /// 模型验证
    /// </summary>
    public class ModelValidateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 在调用操作方法之前发生。
        /// </summary>
        /// <param name="actionContext">操作上下文。</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var errorMessages = from c in actionContext.ModelState
                                    from e in c.Value.Errors
                                    select e.ErrorMessage;

                var value = new HttpResponse<object>()
                {
                    Message = string.Join(string.Empty, errorMessages),
                    Status = WcsHttpResponseStatus.Exceptional,
                    Data = null
                };

                if (string.IsNullOrEmpty(value.Message))
                {
                    value.Message = "输入参数校验失败。";
                }
#if DEBUG
                //调试模式下，打印最详细的信息
                errorMessages = from c in actionContext.ModelState
                                from e in c.Value.Errors
                                select e.ErrorMessage + c.Key + e.Exception?.Message;
                value.Message = string.Join(string.Empty, errorMessages);

#endif


                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, value);
            }
        }
    }
}