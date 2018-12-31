namespace WebApplication1.Refit
{
    /// <summary>
    /// 仓位
    /// </summary>
    public class WharehousePositionDTO
    {
        /// <summary>
        /// 主键ID，自增
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 仓位编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 备件仓库编号
        /// </summary>
        public string WareHouseCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WareHouseName { get; set; }
    }
}
