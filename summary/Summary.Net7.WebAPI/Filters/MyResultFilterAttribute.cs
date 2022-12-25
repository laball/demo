using Microsoft.AspNetCore.Mvc.Filters;

namespace Summary.Net7.WebAPI.Filters
{
    public class MyResultFilterAttribute : ResultFilterAttribute
    {
        private readonly ILogger<MyResultFilterAttribute> _logger;

        public MyResultFilterAttribute(ILogger<MyResultFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }


        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
