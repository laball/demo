namespace WebApplication1.Refit
{
    /// <summary>
    /// RestApi
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResponse<T>
    {
        /// <summary>
        /// 状态标志 0：成功；1：失败；2：异常
        /// </summary>
        public WcsHttpResponseStatus Status { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回结果单值
        /// </summary>
        public T Data { get; set; }
    }
}