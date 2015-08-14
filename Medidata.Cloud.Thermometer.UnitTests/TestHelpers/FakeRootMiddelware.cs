using System.Threading.Tasks;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer.UnitTests.TestHelpers
{
    public class FakeRootMiddelware : OwinMiddleware
    {
        public FakeRootMiddelware() : base(null)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            return Task.FromResult(context);
        }
    }
}