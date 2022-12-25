using Microsoft.AspNetCore.Mvc.Filters;
using Summary.Net7.WebAPI.Controllers;

namespace Summary.Net7.WebAPI.Filters
{
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger<MyActionFilterAttribute> _logger;

        public MyActionFilterAttribute(ILogger<MyActionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("MyActionFilterAttribute.OnActionExecuting");

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("MyActionFilterAttribute.OnActionExecuted");

            base.OnActionExecuted(context);
        }
    }
}
