namespace Lee.Abp.Web.Common
{
    public enum WcsHttpResponseStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 1,
        /// <summary>
        /// 异常
        /// </summary>
        Exceptional = 2,
        /// <summary>
        /// 操作超时
        /// </summary>
        OperateExpired = 3
    }
}
