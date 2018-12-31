using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Abp.Runtime.Security;
using log4net;

namespace AbpSignalrApi
{
    public class HttpBasicAuthorizeAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //对于添加了允许匿名访问的API，直接通过；
            var allowAnonoymous = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (allowAnonoymous)
            {
                IsAuthorized(actionContext);
                return;
            }

            //if (actionContext.Request.Headers.Authorization != null &&
            //    actionContext.Request.Headers.Authorization.Parameter != null)
            //{
                HandleBasicAuthorization(actionContext);
            //}
            //else
            //{
            //    log.Debug("未提供权限验证Token！");
            //    HandleUnauthorizedRequest(actionContext);
            //}
        }

        private void HandleBasicAuthorization(HttpActionContext actionContext)
        {
            var identity = new ApiIdentity
            {
                AuthenticationType = "Basic",
                IsAuthenticated = true,
                Name = "Lee",
                UserCode = "125",
                UserName = "Lee",
                WarehouseCode ="123456"
            };

            var principal = new GenericPrincipal(identity, new string[] { });
            //var chain = new Claim(AbpClaimTypes.UserId, user.User_ID.ToString());
            var chain = new Claim(AbpClaimTypes.UserId, "125");
            principal.AddIdentity(new ClaimsIdentity(new[] { chain }) { });
            HttpContext.Current.User = principal; 

            //string userInfo = Encoding.Default.GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
            //if (userInfo == null)
            //{
            //    HandleUnauthorizedRequest(actionContext);
            //}

            //var fields = userInfo.Split(new string[] { AppConfig.TokenTag }, StringSplitOptions.None);
            //if (fields.Length < 2)
            //{
            //    HandleUnauthorizedRequest(actionContext);
            //}

            //var userCode = fields[0];
            //var userPsw = fields[1];

            //see http://docs.autofac.org/en/latest/faq/per-request-scope.html#no-per-request-filter-dependencies-in-web-api
            //var userRepo = actionContext.Request.GetDependencyScope().GetService(typeof(IRepository<User>)) as IRepository<User>;

            //using (var uow = new UnitOfWork(userRepo.Context))
            //{
            //    var user = userRepo.Single(c => c.Code == userCode);
            //    if (user == null || user.Enabled == string.Empty)
            //    {
            //        log.Debug("用户不存在或已被禁用！");
            //        HandleUnauthorizedRequest(actionContext);
            //    }

            //    if (userPsw == user.PassWord)
            //    {
            //        if (user.LastOperateTime != null)
            //        {
            //            if (DateTime.Now.Subtract(user.LastOperateTime.Value).TotalMinutes > AppConfig.OperateExpiredMinutes)
            //            {
            //                log.Debug($"用户{user.Code}，当前时间{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}，上次操作时间{user.LastOperateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")}，登录超时！");
            //                HandleUnauthorizedRequest(actionContext);
            //            }
            //        }

            //        user.LastOperateTime = DateTime.Now;
            //        userRepo.Update(user);
            //        uow.SaveChanges();

            //        var companyRepo = actionContext.Request.GetDependencyScope().GetService(typeof(IRepository<Company>)) as IRepository<Company>;
            //        var company = companyRepo.Single(c => c.Code == user.CompanyCode);

            //        //see https://stevescodingblog.co.uk/basic-authentication-with-asp-net-webapi/
            //        HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user, company?.Level), new string[] { });

            //var principal = new GenericPrincipal(new ApiIdentity(user), new string[] { });
            //var chain = new Claim(AbpClaimTypes.UserId, user.User_ID.ToString());
            //principal.AddIdentity(new ClaimsIdentity(new[] { chain }) { });
            //HttpContext.Current.User = principal;


            //        IsAuthorized(actionContext);
            //    }
            //    else
            //    {
            //        log.Debug("密码错误！");
            //        HandleUnauthorizedRequest(actionContext);
            //    }
            //}
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var challengeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            throw new HttpResponseException(challengeMessage);
        }


        private static readonly ILog log = LogManager.GetLogger(typeof(HttpBasicAuthorizeAttribute));
    }
}