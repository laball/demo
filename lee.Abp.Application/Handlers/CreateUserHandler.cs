using System.Diagnostics;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Lee.Abp.Core.Users.Services;

namespace Lee.Abp.Application.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateUserHandler : IEventHandler<CreateUserEvent>, ITransientDependency
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        private readonly IUserManager _userManager;

        //private volatile


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public CreateUserHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(CreateUserEvent eventData)
        {
            Trace.WriteLine($"create user ,name: {eventData.Name} ,code: {eventData.Code} .");
        }
    }
}
