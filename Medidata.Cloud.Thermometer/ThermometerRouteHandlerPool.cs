using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    internal class ThermometerRouteHandlerPool : IEnumerable<IThermometerHandler>
    {
        private readonly IDictionary<PathString, IThermometerHandler> _dic = new Dictionary<PathString, IThermometerHandler>();

        public ThermometerRouteHandlerPool Add(IThermometerHandler handler)
        {
            var key = new PathString(handler.Question.Route);
            _dic.Add(key, handler);
            return this;
        }

        public bool Contains(string route)
        {
            return _dic.ContainsKey(new PathString(route));
        }

        public IThermometerHandler FindOrDefault(string route)
        {
            IThermometerHandler handler;
            var key = new PathString(route);
            return _dic.TryGetValue(key, out handler) ? handler : null;
        }

        public IEnumerator<IThermometerHandler> GetEnumerator()
        {
            return _dic.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}