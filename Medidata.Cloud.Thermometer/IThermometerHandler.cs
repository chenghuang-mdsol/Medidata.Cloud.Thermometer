using System;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    public interface IThermometerHandler
    {
        string Name { get; }
        PathString RoutePath { get; }
        Func<dynamic, object> Func { get; }
    }
}