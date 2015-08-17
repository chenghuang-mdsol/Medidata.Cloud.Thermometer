using Medidata.Cloud.Thermometer.Middlewares;
using Medidata.Cloud.Thermometer.UnitTests.TestHelpers;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;

namespace Medidata.Cloud.Thermometer.UnitTests.Middlewares
{
    [TestClass]
    public class JsonResponseMiddlewareTests
    {
        private IFixture _fixture;
        private JsonResponseMiddleware _sut;
        private OwinMiddleware _nextMiddleware;

        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
            _nextMiddleware = MockRepository.GenerateMock<OwinMiddleware>(new FakeRootMiddelware());
            _sut = new JsonResponseMiddleware(_nextMiddleware);
        }

        [TestMethod]
        public void Invoke_sets_ResponseHeaderAsNoCache()
        {
            // Arrange
            var context = _fixture.Create<IOwinContext>();
            context.Stub(x => x.Response).Return(_fixture.Create<IOwinResponse>());
            context.Response.Stub(x => x.Headers).Return(_fixture.Create<IHeaderDictionary>());

            // Act
            var result = _sut.Invoke(context);
            //result.Wait();

            // Assert
            context.AssertWasCalled(x => x.Response.Headers["Content-type"] = "application/json");
            context.AssertWasCalled(x => x.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate");
            context.AssertWasCalled(x => x.Response.Headers["Pragma"] = "no-cache");
            context.AssertWasCalled(x => x.Response.Headers["Expires"] = "0");

            _nextMiddleware.AssertWasCalled(x => x.Invoke(context));
        }
    }
}
