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

        private readonly ThermometerQuestionHandlerPool _handlerPool = new ThermometerQuestionHandlerPool();

        public ThermometerApp Reply(string question, Func<IThermometerQuestion, object> func)
        {
            if (String.IsNullOrWhiteSpace(question)) throw new ArgumentException("Question name cannot be null or empty string.", "question");
            if (func == null) throw new ArgumentNullException("func");

            var key = new PathString(question.Trim());

            if (_handlerPool.ContainsKey(key))
                throw new ArgumentException(String.Format("Question '{0}' has been defined.", question));

            _handlerPool.Add(key, func);

            return this;
        }

        public IDisposable Listen(int port)
        {
            var url = "http://*:" + port;
            return WebApp.Start(url, app =>
                    {
                        app.Use<OnlyHttpGetMiddleware>()
                           .Use<JsonResponseMiddleware>()
                           .Use<QuestionRouteMiddleware>(_handlerPool);
                    });
        }
    }
}