using System;
using System.Collections.Generic;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    public class ThermometerQuestionHandlerPool : Dictionary<PathString, Func<dynamic, object>>
    {
    }
}