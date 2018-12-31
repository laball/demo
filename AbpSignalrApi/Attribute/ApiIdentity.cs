using System.Security.Principal;

namespace AbpSignalrApi
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Security.Principal.IIdentity" />
    public class ApiIdentity : IIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiIdentity"/> class.
        /// </summary>
        public ApiIdentity()
        {

        }

        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string WarehouseCode { get; set; }
    }
}