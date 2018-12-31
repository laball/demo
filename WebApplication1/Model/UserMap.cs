using Core.Entity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebApplication1.Model
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("emms_system_user");
            Schema("emms_dev");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Assigned));
            Property(x => x.Code);
            Property(x => x.Name);
            Property(x => x.CompanyCode);
            Property(x => x.WorkAreaCode);
            Property(x => x.WareHouseCode);
            Property(x => x.Phone);
            Property(x => x.PassWord);
            Property(x => x.Salt);
            Property(x => x.NeedResetPassword);
            Property(x => x.RoleID);
            Property(x => x.LastLoginTime);
            Property(x => x.LastOperateTime);
            Property(x => x.Enabled);
            Property(x => x.CreateUser);
            Property(x => x.CreateTime);
            Property(x => x.ModifyUser);
            Property(x => x.ModifyTime);
            Property(x => x.DeleteFlag);
        }
    }
}

