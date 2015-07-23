using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Extensions;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Middlewares
{
    public class ActionRouteMiddleware : OwinMiddleware
    {
        private readonly ActionRouteConfiguration _config;

        public ActionRouteMiddleware(OwinMiddleware next, ActionRouteConfiguration config)
            : base(next)
        {
            if (config == null) throw new ArgumentNullException("config");
            _config = config;
        }

        public override async Task Invoke(IOwinContext context)
        {
            Func<IDictionary<string, object>, object> func;
            if (_config.TryGetValue(context.Request.Path, out func))
            {
                try
                {
                    var result = func(context.Request.ToDatabag()) ?? new { };
                    context.Response.Write(result.ToJsonString());

                    await Next.Invoke(context);
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var jsonString = new { exception = e.ToString() }.ToJsonString();
                    context.Response.Write(jsonString);
                }
            }
            else
            {
                // Returns 404
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
    }
}