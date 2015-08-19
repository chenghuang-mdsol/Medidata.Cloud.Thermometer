using System.Diagnostics;
using System.Net.Http;
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
    public class OnlyHttpGetMiddlewareTests
    {
        private IFixture _fixture;
        private ThermometerRouteHandlerPool _handlerPool;
        private OwinMiddleware _nextMiddleware;
        private OnlyHttpGetMiddleware _sut;

        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
            _nextMiddleware = MockRepository.GenerateMock<OwinMiddleware>(new FakeRootMiddelware());
            _sut = new OnlyHttpGetMiddleware(_nextMiddleware);
        }

        [TestMethod]
        public void Invoke_ShouldByPassWhenHttpGet()
        {
            // Arrage
            var context = _fixture.Create<IOwinContext>();
            context.Stub(x => x.Request).Return(_fixture.Create<IOwinRequest>());
            context.Request.Stub(x => x.Method).Return(HttpMethod.Get.Method);

            var response = MockRepository.GenerateStub<IOwinResponse>();
            context.Stub(x => x.Response).Return(response);
            context.Response.StatusCode = 200;

            //Act
            _sut.Invoke(context);

            //Assert
            _nextMiddleware.AssertWasCalled(x => x.Invoke(context));
            Debug.Assert(context.Response.StatusCode == 200);
        }

        [TestMethod]
        public void Invoke_ShouldReturnHttp505WhenNotGet()
        {
            // Arrage
            var context = _fixture.Create<IOwinContext>();
            context.Stub(x => x.Request).Return(_fixture.Create<IOwinRequest>());
            context.Request.Stub(x => x.Method).Return(HttpMethod.Delete.Method);

            var response = MockRepository.GenerateStub<IOwinResponse>();
            context.Stub(x => x.Response).Return(response);

            //Act
            _sut.Invoke(context);

            //Assert
            _nextMiddleware.AssertWasNotCalled(x => x.Invoke(context));
            Assert.AreEqual(505, context.Response.StatusCode);
        }
    }
}