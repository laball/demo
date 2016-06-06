using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateDemo
{
    public class DoctorByCodeMapping :ClassMapping<Doctor>
    {
        public DoctorByCodeMapping()
        {
            Table("DOCTOR");
            Lazy(true);
            Id(x => x.ID,map => map.Generator(Generators.Identity));
            Property(x => x.UserName,map => map.Column("USERNAME"));
        }
    }
}
