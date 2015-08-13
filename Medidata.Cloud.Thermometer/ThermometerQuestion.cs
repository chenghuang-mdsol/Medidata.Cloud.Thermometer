using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    internal class ThermometerQuestion : StringDictionary, IThermometerQuestion
    {
        public string Route { get; set; }

        public string Name { get; set; }

        public new IEnumerable<string> Keys
        {
            get { return base.Keys.Cast<string>(); }
        }

        public ThermometerQuestion(string route, string name = null)
        {
            if (String.IsNullOrWhiteSpace(route)) throw new ArgumentException("Question route path cannot be null or empty string.", "route");
            Route = new PathString(route).ToString();
            Name = name ?? route.TrimStart('/').Replace('/', '.');
        }
    }
}