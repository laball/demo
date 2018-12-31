using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Quartz;
using Abp.Runtime.Caching;
using Core.Integrated;
using Lee.Abp.Application.Users;
using Lee.Abp.Application.Users.Dto;
using Lee.Abp.Hangfire.BackgroundJobs;
using Lee.Abp.Quartz.BackgroundJobs;
using Lee.Abp.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Abp.Domain.Repositories;
using Lee.Abp.Core;
using Lee.Abp.EntityFrameworkCore;
using System.Linq;

namespace Lee.Abp.Web.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiGroup("通用", "用户信息")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IQuartzScheduleJobManager _jobManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        private readonly IRepository<Role> roleRepo;

        //private readonly ICacheManager _cacheManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAppService"></param>
        /// <param name="jobManager"></param>
        /// <param name="backgroundJobManager"></param>
        /// <param name="cacheManager"></param>
        public UserController(
            IUserAppService userAppService,
            IQuartzScheduleJobManager jobManager,
            IBackgroundJobManager backgroundJobManager,
            IRepository<Role> roleRepo
            //,            ICacheManager cacheManager
            )
        {
            _userAppService = userAppService;
            _jobManager = jobManager;
            _backgroundJobManager = backgroundJobManager;
            this.roleRepo = roleRepo;
            //_cacheManager = cacheManager;

   



        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        //[AuthorizeAttribute()]
        public async Task<HttpResponse<int>> Append([FromBody] UserAppendInputDto inputDto)
        {
            //var ddd = this.HttpContext.User;

            var roles = roleRepo.GetAllList();



            await _userAppService.Create(inputDto);
            return Success(0);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<HttpResponse<int>> Update(UserUpdateInputDto inputDto)
        {
            await _userAppService.Update(inputDto);
            return Success(0);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Paged")]
        public async Task<HttpPagedResponse<UserOutputDto>> GetUsers(UserQueryInputDto inputDto)
        {
            var result = await _userAppService.GetList(inputDto);
            return Success(result);
        }

        /// <summary>
        /// 调度Hangfire Job
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Job/Hangfire")]
        public async Task<HttpResponse<int>> ScheduleHangfireJob()
        {
            await _backgroundJobManager.EnqueueAsync<HangfireJob, int>(27);
            return Success(0);
        }

        /// <summary>
        /// 调度Quartz Job
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Job/Quartz")]
        public async Task<HttpResponse<int>> ScheduleQuartzJob()
        {
            await _jobManager.ScheduleAsync<QuartzJob>(
                job =>
                {
                    job.WithIdentity("TestJob", "myGroup")
                    .WithDescription("A Test Job");
                },
                trigger =>
                {
                    trigger.StartNow()
                    .WithSimpleSchedule(schedule =>
                    {
                        schedule.RepeatForever()
                            .WithIntervalInSeconds(2)
                            .Build();
                    });
                });

            return Success(0);
        }

        /*

        /// <summary>
        /// 缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Cache/{key}")]
        public async Task<HttpResponse<string>> GetCache(string key)
        {
            var cache = _cacheManager.GetCache("test");
            var value = await cache.GetOrDefaultAsync(key);
            if (value == null)
            {
                var newValue = new System.Random().Next(100, 999);
                await cache.SetAsync(key, newValue);
                value = newValue;
            }

            return Success(value.ToString());
        }

        */

    }
}
