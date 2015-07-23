using System.Web.Script.Serialization;

namespace Medidata.Cloud.Thermometer.Extensions
{
    public static class ObjectExtensions
    {
        internal static string ToJsonString(this object owner)
        {
            return new JavaScriptSerializer().Serialize(owner);
        }
    }
}