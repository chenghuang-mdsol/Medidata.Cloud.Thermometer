using Medidata.Cloud.Thermometer.Middlewares;
using Owin;

namespace Medidata.Cloud.Thermometer
{
    public class Startup
    {
        private readonly ActionRouteConfiguration _actionRouteConfiguration;

        public Startup(ActionRouteConfiguration actionRouteConfiguration)
        {
            _actionRouteConfiguration = actionRouteConfiguration;
        }

        public void Configuration(IAppBuilder app)
        {
            app.Use<OnlyHttpGetMiddleware>()
               .Use<JsonResponseMiddleware>()
               .Use<ActionRouteMiddleware>(_actionRouteConfiguration);
        }
    }
}