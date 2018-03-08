using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Demo")]
    public class DemoController : Controller
    {
    }
}