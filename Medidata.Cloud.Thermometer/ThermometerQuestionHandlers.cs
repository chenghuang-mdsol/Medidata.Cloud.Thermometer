using System;
using System.Collections.Generic;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    public class ThermometerQuestionHandlers : Dictionary<PathString, Func<IThermometerQuestion, object>>
    {
    }
}