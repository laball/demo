using System;

namespace Lee.Abp.Common
{
    public class APIException : Exception
    {
        public APIException() { }

        public APIException(string message)
           : base(message)
        {
        }

        public APIException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
