using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Services;
using Microsoft.Owin.Hosting.Starter;
using Owin;

namespace Medidata.Cloud.Thermometer.Listeners
{
    public class WebApiOnOwinListener : IListen
    {
        public Task<IDisposable> Listen(int port)
        {

            if (port <= 1024) throw new ArgumentException("Must choose a port greater than 1024", "port");
            var options = new StartOptions("http://*:" + port + "/");
            return Task.Run(() => WebAppStart(options));
        }

        
        

        private IDisposable WebAppStart(StartOptions options)
        {
            return WebApp.Start(options, builder =>
            {
                
                HttpConfiguration config = new HttpConfiguration();
                config.MapHttpAttributeRoutes();
                //config.Routes.MapHttpRoute(
                //    name: "DefaultApi",
                //    routeTemplate: "{action}",
                //    defaults: new { id = RouteParameter.Optional }
                //    );
                config.EnsureInitialized();
                builder.UseWebApi(config);

            });
        }

        //Web Api actually doesn't use the Action Route, the actions are in the "Controllers"
        public ActionRouteConfiguration Routes { get; private set; }
    }
}
