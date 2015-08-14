using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class OwinResponseExtensions
    {
        internal static void WriteAsJson(this IOwinResponse owner, object target)
        {
            var obj = target ?? new {};
            var json = new JavaScriptSerializer().Serialize(obj);
            owner.Write(json);
        }
    }
}