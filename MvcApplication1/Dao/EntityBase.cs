using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Dao
{
    public class EntityBase
    {
        #region Public Properties
        /// <summary>
        /// IS_DELETE   
        /// Description: 是否删除：0：否；1：删除     
        /// </summary>
        public virtual bool IsDelete { get; set; }

        /// <summary>
        /// CREATED_BY   
        /// Description: 创建人     
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// CREATED_ON   
        /// Description: 创建时间     
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// MODIFIED_BY   
        /// Description: 最后修改人     
        /// </summary>
        public virtual string ModifiedBy { get; set; }

        /// <summary>
        /// MODIFIED_ON   
        /// Description: 最后修改时间     
        /// </summary>
        public virtual DateTime ModifiedOn { get; set; }

        /// <summary>
        /// VERSION   
        /// Description: 数据记录版本号     
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// GUID   
        /// Description:      
        /// </summary>
        public virtual string Guid { get; set; }
        #endregion
    }
}