using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Middlewares;
using Microsoft.Owin;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Services;
using Microsoft.Owin.Hosting.Starter;
using Owin;

namespace Medidata.Cloud.Thermometer
{
    public class ThermometerApp
    {
        /// <summary>
        /// This unused field is to keep reference of Microsoft.Owin.Host.HttpListener.dll, 
        /// so as the assembly file can be copied to the caller's folder when build.
        /// </summary>
#pragma warning disable 169
        private readonly OwinHttpListener _assemblyAnchor;
#pragma warning restore 169

        private readonly ThermometerRouteHandlerConfiguration _routeHandlerConfig = new ThermometerRouteHandlerConfiguration();

        public ThermometerApp Answer(string route, Func<dynamic, object> func)
        {
            if (String.IsNullOrWhiteSpace(route)) throw new ArgumentException("Question route path cannot be null or empty string.", "route");
            return Answer(route.TrimStart('/').Replace('/', '_'), route, func);
        }

        public ThermometerApp Answer(string name, string route, Func<dynamic, object> func)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException("Question name cannot be null or empty string.", "name");
            if (String.IsNullOrWhiteSpace(route)) throw new ArgumentException("Question route path cannot be null or empty string.", "route");
            if (func == null) throw new ArgumentNullException("func");

            var handler = new ThermometerHandler(route.Trim(), func, name);

            _routeHandlerConfig.Add(handler.RoutePath, handler);

            return this;
        }

        public IDisposable Listen(int port)
        {
            var url = "http://*:" + port;
            return WebApp.Start(url, app =>
                    {
                        app.Use<OnlyHttpGetMiddleware>()
                           .Use<JsonResponseMiddleware>()
                           .Use<ListAllQuestionMiddleware>(_routeHandlerConfig)
                           .Use<QuestionRouteMiddleware>(_routeHandlerConfig);
                    });
        }
    }
}