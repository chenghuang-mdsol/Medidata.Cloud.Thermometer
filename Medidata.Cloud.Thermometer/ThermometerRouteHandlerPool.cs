using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    [ExcludeFromCodeCoverage]
    public class ThermometerRouteHandlerPool : Dictionary<PathString, IThermometerHandler>
    {
    }
}