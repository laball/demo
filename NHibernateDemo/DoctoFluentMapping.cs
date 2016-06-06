using FluentNHibernate.Mapping;

namespace NHibernateDemo
{
    public class DoctoFluentMapping :ClassMap<Doctor>
    {
        public DoctoFluentMapping()
        {
            Table("DOCTOR");
            LazyLoad();
            Id(x => x.ID).GeneratedBy.Identity().Column("ID");
            Map(x => x.UserName).Column("USERNAME");
        }
    }
}
