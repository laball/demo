using System;
using System.Text;
using System.Collections.Generic;


namespace MvcApplication1 {
    
    public class Needredorequest {
        public virtual int? Id { get; set; }
        public virtual string Url { get; set; }
        public virtual string Postdata { get; set; }
        public virtual int? Redocount { get; set; }
        public virtual bool? Issuccess { get; set; }
        public virtual DateTime? CreateOn { get; set; }
        public virtual DateTime? LastRedo { get; set; }
    }
}
