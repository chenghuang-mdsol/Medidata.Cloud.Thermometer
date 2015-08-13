using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Extensions;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Middlewares
{
    public class QuestionRouteMiddleware : OwinMiddleware
    {
        private readonly ThermometerQuestionHandlerPool _handlerPool;

        public QuestionRouteMiddleware(OwinMiddleware next, ThermometerQuestionHandlerPool handlerPool)
            : base(next)
        {
            if (handlerPool == null) throw new ArgumentNullException("handlerPool");
            _handlerPool = handlerPool;
        }

        public override async Task Invoke(IOwinContext context)
        {
            Func<dynamic, object> func;
            if (_handlerPool.TryGetValue(context.Request.Path, out func))
            {
                try
                {
                    var result = func(context.Request.ToThermometerQuestion()) ?? new { };
                    var json = result.ToString();
                    context.Response.Write(json);

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