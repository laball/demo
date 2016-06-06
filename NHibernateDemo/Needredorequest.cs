using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Mapping.Attributes;


namespace NHibernateDemo
{
    [Class]
    public class Needredorequest
    {
        [NHibernate.Mapping.Attributes.Id(Name = "Id",TypeType = typeof(int))]
        [NHibernate.Mapping.Attributes.Generator(1,Class = "identity")]
        public virtual int Id { get; set; }
        [Property(Column = "url")]
        public virtual string Url { get; set; }
        [Property(Column = "postdata",Length = 1073741823)]
        public virtual string Postdata { get; set; }
        [Property(Column = "redoCount")]
        public virtual int? Redocount { get; set; }
        [Property(Column = "IsSuccess")]
        public virtual bool? Issuccess { get; set; }
        [Property(Column = "Create_ON")]
        public virtual DateTime? CreateOn { get; set; }
        [Property(Column = "Last_ReDo")]
        public virtual DateTime? LastRedo { get; set; }
    }
}
