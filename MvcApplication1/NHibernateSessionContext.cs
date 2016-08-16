using NHibernate;
using NHibernate.Cfg;

namespace MvcApplication1
{
    public static class NHibernateSessionContext
    {
        private static readonly object synRoot = new object();
        public static ISessionFactory SessionFactory { get; private set; }
        public static void BuildSessionFactory()
        {
            //double-checked locking
            if(SessionFactory == null)
            {
                lock(synRoot)
                {
                    if(SessionFactory == null)
                    {
                        var nhConfig = new Configuration().Configure();
                        SessionFactory = nhConfig.BuildSessionFactory();


                        //var session = NHibernateSessionContext.SessionFactory.GetCurrentSession();
                        //var items = session.QueryOver<Needredorequest>().List();

                    }
                }
            }
        }
    }
}