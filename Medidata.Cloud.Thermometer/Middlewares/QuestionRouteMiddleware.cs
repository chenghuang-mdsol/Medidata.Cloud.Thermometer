using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Extensions;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Middlewares
{
    internal class QuestionRouteMiddleware : OwinMiddleware
    {
        private readonly ThermometerRouteHandlerPool _handlerSet;

        public QuestionRouteMiddleware(OwinMiddleware next, ThermometerRouteHandlerPool handlerSet)
            : base(next)
        {
            if (handlerSet == null) throw new ArgumentNullException("handlerSet");
            _handlerSet = handlerSet;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var handler = _handlerSet.FindOrDefault(context.Request.Path.ToString());
            if (handler != null)
            {
                try
                {
                    var result = handler.Handler(context.Request.ToThermometerQuestion()) ?? new { };
                    context.Response.WriteAsJson(result);

                    await Next.Invoke(context);
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var result = new { exception = e.ToString() };
                    context.Response.WriteAsJson(result);
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