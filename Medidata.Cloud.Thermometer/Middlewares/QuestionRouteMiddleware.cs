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
        private readonly ThermometerQuestionHandlers _handlers;

        public QuestionRouteMiddleware(OwinMiddleware next, ThermometerQuestionHandlers handlers)
            : base(next)
        {
            if (handlers == null) throw new ArgumentNullException("handlers");
            _handlers = handlers;
        }

        public override async Task Invoke(IOwinContext context)
        {
            Func<IThermometerQuestion, object> func;
            if (_handlers.TryGetValue(context.Request.Path, out func))
            {
                try
                {
                    var result = func(context.Request.ToThermometerQuestion()) ?? new { };
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