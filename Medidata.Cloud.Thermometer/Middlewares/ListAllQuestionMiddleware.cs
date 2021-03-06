﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Extensions;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Middlewares
{
    internal class ListAllQuestionMiddleware : OwinMiddleware
    {
        private readonly ThermometerRouteHandlerPool _handlerSet;

        public ListAllQuestionMiddleware(OwinMiddleware next, ThermometerRouteHandlerPool handlerSet)
            : base(next)
        {
            if (handlerSet == null) throw new ArgumentNullException("handlerSet");
            _handlerSet = handlerSet;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var request = context.Request;
            var baseUrl = request.Uri.ToString().TrimEnd('/');
            if (request.Path.ToString() == "/")
            {
                var list = _handlerSet
                    .Select(kvp => new { name = kvp.Question.Name, url = baseUrl + kvp.Question.Route })
                    .ToList();
                context.Response.WriteAsJson(list);
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}