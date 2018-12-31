using Microsoft.AspNetCore.Http;

namespace Lee.Abp.Utils
{
    /// <summary>
    /// 参见 http://www.cnblogs.com/linezero/p/6801602.html
    /// https://stackoverflow.com/questions/38571032/how-to-get-httpcontext-current-in-asp-net-core
    /// 集成后可在各个层中直接访问；
    /// </summary>
    public static class HttpContext
    {
        private static IHttpContextAccessor _accessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;

        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
