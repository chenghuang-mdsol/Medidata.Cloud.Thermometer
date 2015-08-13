using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Extensions
{
    internal static class OwinRequestExtensions
    {
        internal static IThermometerQuestion ToThermometerQuestion(this IOwinRequest owner)
        {
            var question = new ThermometerQuestion(owner.Path.ToString());

            if (!owner.QueryString.HasValue) return question;

            var queryParams = HttpUtility.ParseQueryString(owner.QueryString.ToString());
            foreach (var key in queryParams.AllKeys)
            {
                question.Add(key, queryParams[key]);
            }

            return question;
        }
    }
}