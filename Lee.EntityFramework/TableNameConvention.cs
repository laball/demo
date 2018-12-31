using System.Data.Entity.ModelConfiguration.Conventions;

namespace Lee.EntityFramework
{
    public class TableNameConvention : Convention
    {
        public TableNameConvention()
        {
            this.Types().Configure(c => c.ToTable("Demo_" + c.ClrType.Name));
        }
    }
}
