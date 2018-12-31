using System;
using System.Collections.Generic;
using System.Text;

namespace Lee.Abp.Core.Common
{
    public interface ISoftDelete
    {
        string DeleteFlag { get; set; }
    }
}
