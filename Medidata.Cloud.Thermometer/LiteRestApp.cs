using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Listeners;
using Microsoft.Owin;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Services;
using Microsoft.Owin.Hosting.Starter;

namespace Medidata.Cloud.Thermometer
{
    public class LiteRestApp
    {
        /// <summary>
        /// This unused field is to keep reference of Microsoft.Owin.Host.HttpListener.dll, 
        /// so as the assembly file can be copied to the caller's folder when build.
        /// </summary>
#pragma warning disable 169
        private readonly OwinHttpListener _assemblyAnchor;
#pragma warning restore 169

        private readonly ActionRouteConfiguration _routes = new ActionRouteConfiguration();
        private Dictionary<string, IListen> _listeners =new Dictionary<string, IListen>();

        public IListen Listener { get; private set; }


        public LiteRestApp Use(IListen listener)
        {
            Listener = listener;
            return this;
        }

        public LiteRestApp WhenGet(string routePath, Func<IDictionary<string, object>, object> func)
        {
            if (func == null) throw new ArgumentNullException("func");
            if (String.IsNullOrWhiteSpace(routePath)) throw new ArgumentException("Cannot be null or empty string.", "routePath");
            
            var key = new PathString(routePath);
            
            if (_routes.ContainsKey(key))
                throw new ArgumentException(String.Format("'{0}' route has been defined.", routePath));

            _routes.Add(key, func);
            
            return this;
        }

        public Task<IDisposable> Listen(int port)
        {
            if (Listener == null)
            {
                Listener = new OwinListener(_routes);
            }
            return Listener.Listen(port);
            if (port <= 1024) throw new ArgumentException("Must choose a port greater than 1024", "port");

            var serviceProvider = (ServiceProvider)ServicesFactory.Create();
            serviceProvider.AddInstance<ActionRouteConfiguration>(_routes);
            var starter = serviceProvider.GetService<IHostingStarter>();

            var options = new StartOptions("http://*:" + port + "/");

            return Task.Run(() => starter.Start(options));
        }

    }
}