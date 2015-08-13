using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    public class ThermometerHandler : IThermometerHandler
    {
        public string Name { get; private set; }

        public PathString RoutePath { get; private set; }

        public Func<dynamic, object> Func { get; private set; }

        public ThermometerHandler(string route, Func<dynamic, object> func, string name)
        {
            if (route == null) throw new ArgumentNullException("route");
            if (func == null) throw new ArgumentNullException("func");
            if (name == null) throw new ArgumentNullException("name");
            RoutePath = new PathString(route);
            Name = name;
            Func = func;
        }

        public override int GetHashCode()
        {
            return RoutePath.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ThermometerHandler;
            return other != null && RoutePath.Equals(other.RoutePath);
        }
    }
}
