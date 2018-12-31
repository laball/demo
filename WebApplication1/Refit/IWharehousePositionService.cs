using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace WebApplication1.Refit
{
    /// <summary>
    /// 
    /// </summary>
    //直接通过特性设置http head
    //[Headers("Authorization:Basic YWRtaW5ANTk4MzY0ODNjODBiNzk5YzExZmU0ZmQyM2VmYjhjNjhAc3VuaW5nJCVed2Nz")]
    public interface IWharehousePositionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDTO"></param>
        /// <returns></returns>
        [Post("/api/v1/WharehousePosition/Paged")]
        Task<HttpPagedResponse<List<WharehousePositionDTO>>> Query([Body] WarehousePositionQueryInputDTO queryDTO);
    }
}
