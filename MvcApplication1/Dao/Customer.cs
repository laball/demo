using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Dao
{
    /// <summary>
    /// <para>Customer Object</para>
    /// <para>Summary description for Customer.</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    ///[Serializable]
    public partial class Customer :EntityBase
    {
        #region Public Properties
        /// <summary>
        /// ID   
        /// Description:      
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// CNAME   
        /// Description: 中文名     
        /// </summary>
        public virtual string Cname { get; set; }

        /// <summary>
        /// GENDER   
        /// Description: 男，女     
        /// </summary>
        public virtual string Gender { get; set; }

        /// <summary>
        /// BIRTHDAY   
        /// Description:      
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// CERTIFICATE_TYPE   
        /// Description: 身份证     
        /// </summary>
        public virtual string CertificateType { get; set; }

        /// <summary>
        /// CERTIFICATE_CODE   
        /// Description:      
        /// </summary>
        public virtual string CertificateCode { get; set; }

        /// <summary>
        /// MOBILE   
        /// Description:      
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// EMAIL   
        /// Description:      
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// ADDRESS   
        /// Description:      
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// NATION   
        /// Description:      
        /// </summary>
        public virtual string Nation { get; set; }

        /// <summary>
        /// CAREER   
        /// Description:      
        /// </summary>
        public virtual string Career { get; set; }

        /// <summary>
        /// CONTACT_MOBILE   
        /// Description:      
        /// </summary>
        public virtual string ContactMobile { get; set; }

        /// <summary>
        /// CONTACT_RELATION   
        /// Description:      
        /// </summary>
        public virtual string ContactRelation { get; set; }

        /// <summary>
        /// CONTACT_NAME   
        /// Description:      
        /// </summary>
        public virtual string ContactName { get; set; }

        /// <summary>
        /// ACCOUNT_ID   
        /// Description: app客户guid     
        /// </summary>
        public virtual string AccountId { get; set; }

        /// <summary>
        /// NICKNAME   
        /// Description: app客户昵称     
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// PHOTO_URL   
        /// Description: 头像存储地址     
        /// </summary>
        public virtual string PhotoUrl { get; set; }

        /// <summary>
        /// MARITAL_STATUS   
        /// Description: 0：未婚，1：已婚     
        /// </summary>
        public virtual string MaritalStatus { get; set; }

        /// <summary>
        /// PREGNANCE_STATUS   
        /// Description: 0：未孕，1：备孕，2：已孕     
        /// </summary>
        public virtual string PregnanceStatus { get; set; }

        /// <summary>
        /// COMPANY_NAME   
        /// Description:      
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// WEIGHT   
        /// Description:      
        /// </summary>
        public virtual decimal? Weight { get; set; }

        /// <summary>
        /// HEIGHT   
        /// Description:      
        /// </summary>
        public virtual decimal? Height { get; set; }

        /// <summary>
        /// REGIST_ON   
        /// Description: 优健康客户注册时间     
        /// </summary>
        public virtual DateTime? RegistOn { get; set; }

        /// <summary>
        /// CUST_CODE   
        /// Description: 客户体检号,与体检系统对接     
        /// </summary>
        public virtual string CustCode { get; set; }

        #endregion
    }
}