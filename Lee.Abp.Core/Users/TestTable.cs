using System.ComponentModel.DataAnnotations.Schema;
using Lee.Abp.Core.Common;

namespace Lee.Abp.Core.Users
{
    public class TestTable : BaseEntity
    {
        [Column(Order = 1)]
        public int Test { get; set; }

        [Column(Order = 2)]
        public int Test1 { get; set; }
    }
}
