using Abp.Domain.Entities;

namespace Lee.Abp.Core.Common
{
    public class BaseEntityWithTenant<TPrimaryKey> : BaseEntity<TPrimaryKey>, IMustHaveTenant
    {
        public int TenantId { get; set; }
    }

    public class BaseEntityWithTenant : BaseEntityWithTenant<int>
    {

    }
}