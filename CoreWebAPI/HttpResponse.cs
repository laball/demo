using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI
{
    public class HttpResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }
    }
}