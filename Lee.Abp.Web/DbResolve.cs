using System.Collections.Generic;
using Abp.Runtime.Session;
using Lee.Abp.EntityFrameworkCore;

namespace Lee.Abp.Web
{
    public class DbResolve : IDbResolve
    {
        private readonly IAbpSession abpSession;
        private IDictionary<int, string> ConnectionStrings = new Dictionary<int, string>
            (
            new[]
            {
                new KeyValuePair<int, string>(1, "Data Source=10.27.225.165;port=3306;Initial Catalog=emms_test_1;user id=wcsuser;password=wcsuser;charset=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=none"),
                new KeyValuePair<int, string>(2, "Data Source=10.27.225.165;port=3306;Initial Catalog=emms_test_2;user id=wcsuser;password=wcsuser;charset=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=none"),
                new KeyValuePair<int, string>(3, "Data Source=10.27.225.165;port=3306;Initial Catalog=emms_test_3;user id=wcsuser;password=wcsuser;charset=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=none")
            }
            );

        public DbResolve(IAbpSession abpSession)
        {
            this.abpSession = abpSession;
        }

        public string GetConnectionString()
        {

            // TODO:此处可以换成通过主数据库切换各个仓库数据库连接信息
            return ConnectionStrings[abpSession.TenantId.Value];
        }
    }

}
