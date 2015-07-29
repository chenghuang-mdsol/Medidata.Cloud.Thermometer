using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Services;
using Microsoft.Owin.Hosting.Starter;

namespace Medidata.Cloud.Thermometer.Listeners
{
    public class OwinListener: IListen
    {
        
        public OwinListener(ActionRouteConfiguration routes)
        {
            Routes = routes;
        }
        public Task<IDisposable> Listen(int port)
        {
            if (port <= 1024) throw new ArgumentException("Must choose a port greater than 1024", "port");

            var serviceProvider = (ServiceProvider)ServicesFactory.Create();
            serviceProvider.AddInstance<ActionRouteConfiguration>(Routes);
            var starter = serviceProvider.GetService<IHostingStarter>();

            var options = new StartOptions("http://*:" + port + "/");

            return Task.Run(() => starter.Start(options));
        }

        public ActionRouteConfiguration Routes { get; private set; }
    }
}
