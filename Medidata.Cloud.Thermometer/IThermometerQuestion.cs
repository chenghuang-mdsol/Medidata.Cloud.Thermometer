using System.Collections.Generic;

namespace Medidata.Cloud.Thermometer
{
    public interface IThermometerQuestion
    {
        string Name { get; }
        string this[string key] { get; }
        IEnumerable<string> Keys { get; }
    }
}