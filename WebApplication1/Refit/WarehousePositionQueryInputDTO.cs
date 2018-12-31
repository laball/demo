using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Refit
{
    /// <summary>
    /// 仓位查询输入DTO
    /// </summary>
    public class WarehousePositionQueryInputDTO
    {
        /// <summary>
        /// 分页索引，从1开始；
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小，每页数量
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
        public string CompanyCode { get; set; }
        public string WareHouseCode { get; set; }

        //public string Code { get; set; }
        //public string Name { get; set; }
    }
}
