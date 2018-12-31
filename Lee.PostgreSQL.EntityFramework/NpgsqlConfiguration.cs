using System.Data.Entity;
using Npgsql;

namespace Lee.PostgreSQL.EntityFramework
{
    /// <summary>
    /// see http://zacg.github.io/blog/2016/06/04/postgres-and-entity-framework-code-first/
    /// see http://www.cnblogs.com/znlgis/p/3952673.html
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbConfiguration" />
    public class NpgsqlConfiguration : DbConfiguration
    {
        public NpgsqlConfiguration()
        {
            SetProviderServices("Npgsql", NpgsqlServices.Instance);
            SetProviderFactory("Npgsql", NpgsqlFactory.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());

            Loaded += (sender, args) =>
            {

            };
        }
    }
}
