using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidata.Cloud.Thermometer.WebApiOnOwin
{
    public class ThermometerResult
    {
        public string Result { get; set; }
        public string Message { get;set; }
        public string Exception { get; set; }
    }
}
