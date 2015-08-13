using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Extensions
{
    public static class OwinResponseExtensions
    {
        internal static void WriteAsJson(this IOwinResponse owner, object target)
        {
            var obj = target ?? new {};
            var json = new JavaScriptSerializer().Serialize(obj);
            owner.Write(json);
        }
    }
}