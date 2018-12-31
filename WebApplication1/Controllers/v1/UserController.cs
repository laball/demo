namespace WebApplication1.Controllers.v1
{
    using Core.Dto;
    using Core.Service;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using WebApplication1.Refit;

    /// <summary>
    /// 用户信息
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiGroup("通用", "用户信息")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IDistributedCache distributedCache;
        private readonly IWharehousePositionService wharehousePositionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="distributedCache"></param>
        /// <param name="wharehousePositionService"></param>
        public UserController(IUserService userService, IDistributedCache distributedCache, IWharehousePositionService wharehousePositionService)
        {
            this.userService = userService;
            this.distributedCache = distributedCache;
            this.wharehousePositionService = wharehousePositionService;
        }

        [HttpPost]
        public int CreateUser(CreateUserInputDTO createUserInputDTO)
        {
            return userService.CreateUser(createUserInputDTO);
        }

        [HttpGet]
        [Route("{userID:int}")]
        public UserOutputDTO GetUserByID(int userID)
        {
            //var rd = new System.Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    _distributedCache.SetString("test_"+ (i+1).ToString(), rd.Next(100,999).ToString());
            //}

            //WRONGTYPE Operation against a key holding the wrong kind of value
            //see http://www.cnblogs.com/xishuai/p/asp-net-core-use-redis.html
            //var value = _distributedCache.GetString("test_3");

            //******************************************************************
            //var para = new WarehousePositionQueryInputDTO
            //{
            //    PageIndex = 1,
            //    PageSize = 10//,
            //    //CompanyCode = "6676",
            //    //WareHouseCode = "P02"
            //};

            //调用Refit接口
            //var result = _wharehousePositionService.Query(para).Result;
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            //Trace.WriteLine(json);
            //******************************************************************

            return userService.GetUserByID(userID);
        }

        [HttpDelete]
        [Route("{userID:int}")]
        public int DeleteByID(int userID)
        {
            return 1;
        }
    }
}