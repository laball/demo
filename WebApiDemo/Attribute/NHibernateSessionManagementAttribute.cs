using NHibernate;
using NHibernate.Context;
using System.Diagnostics;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiDemo
{

    /// <summary>
    /// 使用Action Filter的方式可以实现NHibernate Sesssion的自动管理，
    /// 但是由于NHibernate本身不支持异步获取数据，不能在Controlller层使用async await方式
    /// 只能手动通过代码管理NHibernate Sesssion
    /// </summary>
    public class NHibernateSessionManagementAttribute : ActionFilterAttribute
    {
        public NHibernateSessionManagementAttribute()
        {
            SessionFactory = NHibernateSessionContext.SessionFactory;
        }

        private ISessionFactory SessionFactory { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            session.BeginTransaction();

            Trace.WriteLine(string.Format("Thread:{0} Session:{1} OnActionExecuting", Thread.CurrentThread.ManagedThreadId, session.GetHashCode()));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var session = SessionFactory.GetCurrentSession();
            //var transaction = session.Transaction;
            //if (transaction != null && transaction.IsActive)
            //{
            //    transaction.Commit();
            //}

            Trace.WriteLine(string.Format("Thread:{0} Session:{1} OnActionExecuted", Thread.CurrentThread.ManagedThreadId, session.GetHashCode()));

            session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
        }
    }
}