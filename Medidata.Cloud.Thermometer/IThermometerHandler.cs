using System;

namespace Medidata.Cloud.Thermometer
{
    internal interface IThermometerHandler
    {
        IThermometerQuestion Question { get; }
        Func<IThermometerQuestion, object> Handler { get; }
    }
}