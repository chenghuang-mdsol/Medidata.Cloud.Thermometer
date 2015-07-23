using System.Threading.Tasks;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.Middlewares
{
    public class JsonResponseMiddleware : OwinMiddleware
    {
        public JsonResponseMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            // No caching
            context.Response.Headers["Content-type"] = "application/json";
            context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            context.Response.Headers["Pragma"] = "no-cache";
            context.Response.Headers["Expires"] = "0";
            await Next.Invoke(context);
        }
    }
}