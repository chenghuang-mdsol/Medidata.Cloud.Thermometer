using System.Collections.Generic;
using System.Web;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Extensions
{
    public static class OwinRequestExtensions
    {
        internal static IThermometerQuestion ToThermometerQuestion(this IOwinRequest owner)
        {
            var question = new ThermometerQuestion { Name = owner.Path.ToString() };

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