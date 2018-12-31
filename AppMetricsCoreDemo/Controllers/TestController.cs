using System;
using Microsoft.AspNetCore.Mvc;

namespace AppMetricsCoreDemo
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        [HttpPost]
        public JsonResult Add([FromBody] CalcItem calcItem)
        {
            MockException();

            var result = calcItem.A + calcItem.B;
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Sub([FromBody] CalcItem calcItem)
        {
            MockException();

            var result = calcItem.A - calcItem.B;
            return new JsonResult(result);
        }

        [NonAction]
        void MockException()
        {
            var rd = new Random();
            var seed = rd.Next(1, 3);
            if (seed == 1)
            {
                throw new Exception("Random Exception");
            }
        }

    }


    public class CalcItem
    {
        public int A { get; set; }
        public int B { get; set; }
    }

}