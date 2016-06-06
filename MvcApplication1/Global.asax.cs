using MvcApplication1.Attribute;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace MvcApplication1
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var format = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Trace.WriteLine(e.ExceptionObject.ToString());
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Trace.WriteLine(e.Exception.StackTrace);
            };

            AreaRegistration.RegisterAllAreas();

            NHibernateSessionContext.BuildSessionFactory();

            GlobalConfiguration.Configuration.Filters.Add(new NHibernateSessionManagementAttribute());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Init()
        {
            //手动开启HtttpContext的Session功能
            //this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);

            base.Init();
        }
    }
}