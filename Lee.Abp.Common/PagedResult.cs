using System.Collections.Generic;

namespace Lee.Abp.Common
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// 查询到的列表总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }

        public PagedResult()
        {
        }

        public PagedResult(int totalCount, int pageSize, List<T> data)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            Data = data;
        }
    }
}
