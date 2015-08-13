using System.Collections.Generic;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    public interface IThermometerQuestion
    {
        string Route { get; }
        string Name { get; }
        string this[string key] { get; }
        IEnumerable<string> Keys { get; }
    }
}