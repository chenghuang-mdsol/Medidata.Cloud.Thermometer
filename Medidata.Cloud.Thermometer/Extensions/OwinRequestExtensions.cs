using System.Collections.Generic;
using System.Web;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Extensions
{
    public static class OwinRequestExtensions
    {
        internal static IDictionary<string, object> ToDatabag(this IOwinRequest owner)
        {
            var dic = new Dictionary<string, object>(owner.Environment);
            if (owner.QueryString.HasValue)
            {
                var queryParams = HttpUtility.ParseQueryString(owner.QueryString.ToString());
                foreach (var key in queryParams.AllKeys)
                {
                    dic.Add(key, queryParams[key]);
                }
            }
            return dic;
        }
    }
}