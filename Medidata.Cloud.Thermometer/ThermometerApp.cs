using System;
using Medidata.Cloud.Thermometer.Middlewares;
using Microsoft.Owin.Hosting;
using Owin;

namespace Medidata.Cloud.Thermometer
{
    public class ThermometerApp
    {
        private readonly ThermometerRouteHandlerPool _routeHandlerPool = new ThermometerRouteHandlerPool();

        public ThermometerApp Answer(string route, Func<IThermometerQuestion, object> func)
        {
            return AnswerImpl(route, func);
        }

        public ThermometerApp Answer(string name, string route, Func<IThermometerQuestion, object> func)
        {
            return AnswerImpl(route, func, name);
        }

        private ThermometerApp AnswerImpl(string route, Func<IThermometerQuestion, object> func, string name = null)
        {
            var question = new ThermometerQuestion(route, name);
            var handler = new ThermometerHandler(question, func);
            _routeHandlerPool.Add(handler);
            return this;
        }

        public IDisposable Listen(int port)
        {
            var url = "http://*:" + port;
            return WebApp.Start(url, app =>
                    {
                        app.Use<OnlyHttpGetMiddleware>()
                           .Use<JsonResponseMiddleware>()
                           .Use<ListAllQuestionMiddleware>(_routeHandlerPool)
                           .Use<QuestionRouteMiddleware>(_routeHandlerPool);
                    });
        }
    }
}