using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI
{
    public class BaseController : Controller
    {
        [NonAction]
        public HttpResponse<T> Success<T>(T data, string message = "")
        {
            return new HttpResponse<T>
            {
                Data = data,
                Message = message,
                Status = 0
            };
        }
    }
}