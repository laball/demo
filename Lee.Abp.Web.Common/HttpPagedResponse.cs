using System.Collections.Generic;

namespace Lee.Abp.Web.Common
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpPagedResponse<T> : HttpResponse<List<T>>
    {
        /// <summary>
        /// 查询总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 分页总数
        /// </summary>
        public int PageCount { get; set; }
    }
}
