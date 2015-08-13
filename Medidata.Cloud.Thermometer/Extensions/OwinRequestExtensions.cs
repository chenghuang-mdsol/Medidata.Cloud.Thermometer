using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Extensions
{
    public static class OwinRequestExtensions
    {
        internal static dynamic ToThermometerQuestion(this IOwinRequest owner)
        {
            dynamic question = new ExpandoObject();
            question.Name = owner.Path.ToString();
            question.Keys = new List<string>();

            if (!owner.QueryString.HasValue) return question;

            var dic = (IDictionary<string, object>) question;
            var queryParams = HttpUtility.ParseQueryString(owner.QueryString.ToString());
            foreach (var key in queryParams.AllKeys)
            {
                dic[key] = queryParams[key];
                question.Keys.Add(key);
            }

            return question;
        }
    }
}