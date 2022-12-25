using Microsoft.AspNetCore.Mvc.Filters;

namespace Summary.Net7.WebAPI.Filters
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<MyExceptionFilterAttribute> _logger;

        public MyExceptionFilterAttribute(ILogger<MyExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError("OnExceptionAsync", context.Exception);

            return base.OnExceptionAsync(context);
        }
    }
}
