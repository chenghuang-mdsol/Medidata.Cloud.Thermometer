using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Medidata.Cloud.Thermometer.WebApiOnOwin;
using Newtonsoft.Json;

namespace Medidata.Cloud.Thermometer.Controllers
{
    public class ThermometerController : ApiController
    {
            //        var app = new LiteRestApp();
            //app
            //    .WhenGet("/HowManyCpus", databag =>
            //    {
            //        var msg = String.Join(", ", databag.Select(x => String.Format("[{0}: {1}]", x.Key, x.Value)));
            //        return new {result = "OK", message = msg};
            //    })
            //    .WhenGet("/CanAccessDb", databag => new {result = "NG", message = "Exception message"})
            //    .WhenGet("/GetException", databag =>
            //    {
            //        throw new Exception("Intential exception for demo");
            //    })
            //    .WhenGet("/CacheFlush", databag =>
            //    {
            //        // Call Rave's cache flush funciton.
            //        return new {result = "OK", message = "Cache flush is done."};
            //    })
            //    .Listen(9000);
        [Route("~/HowManyCpus")]
        [HttpGet]
        public ThermometerResult HowManyCpus()
        {
            var result = new ThermometerResult() {Exception = null, Message = "4 Cpus", Result = "OK"};
            return result;
        }

        [HttpGet]
        [Route("~/CanAccessDb")]
        public ThermometerResult CanAccessDb()
        {
            var result = new ThermometerResult() {Exception = null, Message = "Yes", Result = "OK"};
            return result;
        }

        [HttpGet]
        [Route("~/GetException")]
        public ThermometerResult GetException()
        {
            throw new Exception("Intentional exception for demo");
        }

        [HttpGet]
        [Route("~/GetExceptionWrapped")]
        public ThermometerResult GetExceptionWrapped()
        {
            var result = new ThermometerResult(){Exception = "Intentional exception for demo, wrapped", Message = null, Result = "Internal Service Error"};
            return result;
        }

        [HttpGet]
        [Route("~/CacheFlush")]
        public ThermometerResult CacheFlush()
        {
            var result = new ThermometerResult() {Exception = null, Message = "Cache flush in done"};
            return result;
        }

       
    }
}
