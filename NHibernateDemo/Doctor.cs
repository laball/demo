using NHibernate.Mapping.Attributes;

namespace NHibernateDemo
{
    [Class]
    public class Doctor
    {
        //ID必须要有TypeType，否则会报错“No type name specified”，奇怪的是，官方网站上面居然没有
        [Id(TypeType = typeof(int))]
        //[Generator(0,Class = "Identity")]
        public virtual int ID { get; set; }
        [Property]
        public virtual string UserName { get; set; }
    }
}
