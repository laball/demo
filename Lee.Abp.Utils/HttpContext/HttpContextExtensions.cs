namespace Lee.Abp.Utils
{
    public static class HttpContextExtensions
    {
        public static string GetCurrentUserCode(this Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            return HttpContext.Current?.User?.FindFirst("UserCode")?.Value;
        }
    }
}
