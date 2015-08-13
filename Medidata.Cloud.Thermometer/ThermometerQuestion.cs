using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Medidata.Cloud.Thermometer
{
    internal class ThermometerQuestion : StringDictionary, IThermometerQuestion
    {
        public string Name { get; set; }

        public new IEnumerable<string> Keys
        {
            get { return base.Keys.Cast<string>(); }
        }
    }
}