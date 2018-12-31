using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Lee.Abp.Application.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.Application.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagedQueryExtentions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="source"></param>
        /// <param name="pagedQuery"></param>
        /// <returns></returns>
        public static async Task<Abp.Common.PagedResult<TDTO>> Paged<TEntity, TDTO>(
            this IQueryable<TEntity> source, PagedQueryInputDtoBase pagedQuery)
        {
            var count = await source.CountAsync();
            var order = $"{pagedQuery.SortBy} {pagedQuery.OrderBy}";
            var skipCount = (pagedQuery.PageNumber - 1) * pagedQuery.PageSize;
            var list = await source.OrderBy(order)
                .PageBy(skipCount, pagedQuery.PageSize)
                .ToListAsync();

            return new Abp.Common.PagedResult<TDTO>(count, pagedQuery.PageSize, list.MapTo<List<TDTO>>());
        }
    }
}
