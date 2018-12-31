using System.Threading.Tasks;
using Core.Dao;
using Core.Entity;
using log4net;
using MassTransit;

namespace WebApplication1.MT
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateUserMessageConsumer : IConsumer<ICreateUserMessage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateUserMessageConsumer));

        private readonly IRepository<User> _userRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepo"></param>
        public CreateUserMessageConsumer(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Consume(ConsumeContext<ICreateUserMessage> context)
        {
            //方式1 Task.Factory.StartNew
            //return Task.Factory.StartNew(() =>
            //{
            //    var user = _userRepo.Get(19);
            //    log.Info($"UserName:{user.Name}");

            //    log.Info($"CreateUserMessage ID:{context.Message.UserName},Name:{context.Message.Mail}");
            //});

            //方式2 Task.CompletedTask
            var user = _userRepo.Single(c => c.DeleteFlag == string.Empty);
            log.Info($"UserName:{user.Name}");

            log.Info($"CreateUserMessage ID:{context.Message.UserName},Name:{context.Message.Mail}");

            return Task.CompletedTask;
        }
    }
}
