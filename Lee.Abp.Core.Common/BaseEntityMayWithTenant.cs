using Abp.Domain.Entities;

namespace Lee.Abp.Core.Common
{
    public class BaseEntityMayWithTenant<TPrimaryKey> : BaseEntity<TPrimaryKey>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
    }

    public class BaseEntityMayWithTenant : BaseEntityMayWithTenant<int>
    {

    }
}