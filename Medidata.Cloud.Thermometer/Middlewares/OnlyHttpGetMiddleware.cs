using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Middlewares
{
    public class OnlyHttpGetMiddleware : OwinMiddleware
    {
        public OnlyHttpGetMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (context.Request.Method == HttpMethod.Get.Method)
            {
                await Next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.HttpVersionNotSupported;
            }
        }
    }
}