using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using System.Web.Routing;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Web;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.WebApi;
using AbpSignalrApi.Signalr;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR;
using Microsoft.Web.Http.Routing;

namespace AbpSignalrApi.App_Start
{
    [DependsOn(
        typeof(AbpWebSignalRModule),
        typeof(AbpWebMvcModule),
        typeof(AbpWebApiModule),
        typeof(AbpWebModule)
        )]
    public class AbpSignalrWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            var httpConfiguration = Configuration.Modules.AbpWebApi().HttpConfiguration;

            var cors = new EnableCorsAttribute("*", "*", "*");
            httpConfiguration.EnableCors(cors);

            var constraintResolver = new DefaultInlineConstraintResolver() { ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) } };
            httpConfiguration.AddApiVersioning(o => o.ReportApiVersions = true);
            httpConfiguration.MapHttpAttributeRoutes(constraintResolver);

            httpConfiguration.Routes.MapHttpRoute(
                "VersionedUrl",
                "api/v{apiVersion}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { apiVersion = new ApiVersionRouteConstraint() });

            httpConfiguration.UseSwagger();

            httpConfiguration.Filters.Add(new HttpBasicAuthorizeAttribute());

            //var hubRegist = Component.For<IHubContext<IDispatcher>>()
            //    .Instance(GlobalHost.ConnectionManager.GetHubContext<MyChatHub, IDispatcher>());

            //IocManager.IocContainer.Register(hubRegist);

            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}