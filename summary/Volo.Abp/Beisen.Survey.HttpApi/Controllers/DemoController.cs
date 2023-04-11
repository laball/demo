using Microsoft.AspNetCore.Mvc;

namespace Beisen.Survey.HttpApi.Controllers
{
    //[Area(BookStoreRemoteServiceConsts.ModuleName)]
    //[RemoteService(Name = BookStoreRemoteServiceConsts.RemoteServiceName)]
    //[ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    [ControllerName("Demo")]
    [Route("api/Survey/Demo")]
    public class DemoController : SurveyBaseController
    {
        [HttpGet("Test")]
        public async Task<bool> Test(int Id)
        {
            return await Task.FromResult(true);
        }

        [HttpGet("Add")]
        public async Task<int> Add(int a, int b)
        {
            return await Task.FromResult(a + b);
        }
    }
}